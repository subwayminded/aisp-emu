using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using NLog;

namespace AISpace.Common.Network;
public class TcpServer(string IPAddress, int Port)
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly TcpListener _listener = new(System.Net.IPAddress.Parse(IPAddress), Port);
    private readonly Channel<PacketContext> _packetChannel = Channel.CreateBounded<PacketContext>(1000);
    private readonly CancellationTokenSource _cts = new();

    public ChannelReader<PacketContext> PacketReader => _packetChannel.Reader;

    private readonly ConcurrentDictionary<Guid, ClientContext> _clients = new();

    public void Start()
    {
        _listener.Start();
        _logger.Info($"Server started on {_listener.LocalEndpoint}");

        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync(_cts.Token);
                var context = new ClientContext(Guid.NewGuid(), client.Client.RemoteEndPoint!, client.GetStream());

                _clients[context.Id] = context;
                _ = HandleClientAsync(context); // fire and forget
            }
        });
    }

    public void Stop()
    {
        _cts.Cancel();
        _listener.Stop();
        _packetChannel.Writer.Complete();
    }

    private async Task HandleClientAsync(ClientContext context)
    {

        using var stream = context.Stream;
        var buffer = new byte[4096];

        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                // read length (1 byte)
                int read = await stream.ReadAsync(buffer.AsMemory(0, 1), _cts.Token);

                if (read == 0) break;

                int packetLength = buffer[0];

                if (packetLength < 2) continue;

                await ReadExactAsync(stream, buffer.AsMemory(0, 2), _cts.Token);
                ushort type = BinaryPrimitives.ReadUInt16LittleEndian(buffer.AsSpan(0, 2));
                int payloadLength = packetLength - 2;
                byte[] payload = new byte[payloadLength];
                if (payloadLength > 0)
                    await ReadExactAsync(stream, payload, _cts.Token);

                _packetChannel.Writer.TryWrite(new PacketContext(context, type, payload));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Client {context.Id} error: {ex.Message}");
        }

        context.Stream.Close();
        Console.WriteLine($"Client disconnected: {context.RemoteEndPoint} ({context.Id})");
    }

    private static async Task ReadExactAsync(NetworkStream stream, Memory<byte> buffer, CancellationToken ct)
    {
        int totalRead = 0;
        while (totalRead < buffer.Length)
        {
            int read = await stream.ReadAsync(buffer[totalRead..], ct);
            if (read == 0) throw new IOException("Disconnected");
            totalRead += read;
        }
    }

}
