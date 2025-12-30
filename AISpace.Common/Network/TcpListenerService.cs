using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Encoders;

namespace AISpace.Common.Network;

public record AuthChannel(Channel<Packet> Channel);
public record MsgChannel(Channel<Packet> Channel);
public record AreaChannel(Channel<Packet> Channel);

public class TcpListenerService(ILogger<TcpListenerService> logger,
        Channel<Packet> channel, string Name,
        int port,
        ILoggerFactory loggerFactory) : BackgroundService
{
    private readonly TcpListener _tcpListener = new(System.Net.IPAddress.Parse("0.0.0.0"), port);
    private readonly CancellationTokenSource _cts = new();
    private readonly bool Encrypted = true;

    public ChannelReader<Packet> PacketReader => channel.Reader;

    private readonly ConcurrentDictionary<Guid, ClientConnection> _clients = new();

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();
        _tcpListener.Stop();
        channel.Writer.Complete();
        return base.StopAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _tcpListener.Start();
        logger.LogInformation("Server {name} started on {LocalEP}", Name, _tcpListener.LocalEndpoint);


        while (!_cts.Token.IsCancellationRequested)
        {
            var client = await _tcpListener.AcceptTcpClientAsync(_cts.Token);
            var context = new ClientConnection(Guid.NewGuid(), client.Client.RemoteEndPoint!, client.GetStream(), loggerFactory.CreateLogger<ClientConnection>());
            _clients[context.Id] = context;
            if (Encrypted)
                _ = HandleClientKeyExchangeAsync(context);
            else
                _ = HandleClientAsync(context);

        }
    }

    private async Task HandleClientKeyExchangeAsync(ClientConnection context)
    {
        const int RsaSize = 16;

        logger.LogInformation("New Client. Starting Key Exchange");
        logger.LogInformation("Reading client RSA public key (N, 16 bytes LE)");

        byte[] rsaNLe = new byte[RsaSize];
        await ReadExactAsync(context.Stream, rsaNLe, _cts.Token);

        // Match Rust: two keys (can be same if you want, but do it correctly)
        var (s2cPlain, s2cEnc) = CryptoUtils.CreateEncryptedKey(rsaNLe);
        var (c2sPlain, c2sEnc) = CryptoUtils.CreateEncryptedKey(rsaNLe);

        // IMPORTANT: store PLAINTEXT keys locally (Rust initializes with plaintext)
        context.SetCamelliaKeys(s2cPlain, c2sPlain);

        // IMPORTANT: send CIPHERTEXT keys to client (Rust sends encrypted versions)
        await context.SendRawAsync([.. s2cEnc, .. c2sEnc]);

        _ = HandleClientAsync(context);
    }




    private async Task HandleClientAsync(ClientConnection context)
    {
        logger.LogInformation("{name} Handling new client {Id}", Name, context.Id);

        var stream = context.Stream;
        var header = new byte[4];
        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                await ReadExactAsync(stream, header, _cts.Token);
                int msgSize = (int)BinaryPrimitives.ReadUInt32LittleEndian(header);
                logger.LogInformation("Datasize1: {size}", msgSize+4);
                logger.LogInformation("Datasize2: {size}", msgSize);

                if (msgSize <= 0)
                    continue;

                // 2) Read padded ciphertext
                byte[] cipher = new byte[msgSize];
                await ReadExactAsync(stream, cipher, _cts.Token);
                for (int offset = 0; offset < msgSize; offset += 16)
                    context.DecryptBlock(cipher.AsSpan(offset, 16));
                var plainBuffer = cipher[2..msgSize];
                logger.LogInformation("Datasize3: {size}", (int)BinaryPrimitives.ReadUInt16LittleEndian(cipher.AsSpan(0,2)));
                logger.LogInformation("Datasize4: {size}", plainBuffer.Length);
                ushort typeShort = BinaryPrimitives.ReadUInt16LittleEndian(plainBuffer.AsSpan(0, 2));
                var payload = plainBuffer.AsSpan(2, plainBuffer.Length - 2);
                var type = (PacketType)typeShort;
                logger.LogInformation("Recieving packet {PacketType} ({Length} bytes): {Hex}", type, payload.Length, BitConverter.ToString(payload.ToArray()));
                logger.LogInformation("Recieving Raw packet {PacketType} ({Length} bytes): {Hex}", type, payload.Length, BitConverter.ToString(plainBuffer.ToArray()));
                channel.Writer.TryWrite(new Packet(context, type, payload.ToArray(), typeShort));
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Err {ex}", ex);
        }
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
