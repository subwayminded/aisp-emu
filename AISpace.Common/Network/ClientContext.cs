using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using NLog;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace AISpace.Common.Network;

public enum ClientState
{
    Init = 1,
    ClientSendPublicKey = 2,
    ClientWaitingSharedKeys = 3,
    ServerWaitingKeyExchange = 4,
    ServerGenerateKeys = 5,
    ClientReady = 6,
    ServerReady = 7,
}

public class ClientContext(Guid _Id, EndPoint _RemoteEndPoint, NetworkStream _ns, string _Version = "")
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public CamelliaEngine Camellia = new();
    public KeyParameter? CamelliaKey;
    public ClientState CurrentState;
    public Guid Id = _Id;
    public EndPoint RemoteEndPoint = _RemoteEndPoint;
    public NetworkStream Stream = _ns;
    public string Version = _Version;
    public DateTimeOffset lastPing;

    public async Task SendRawAsync(byte[] data, CancellationToken ct = default)
    {
        await Stream.WriteAsync(data, ct);
    }

    public async Task SendAsync(PacketType type, byte[] data, CancellationToken ct = default)
    {
        var writer = new PacketWriter();
        writer.WriteByte(0x03);
        writer.WriteUIntLE((uint)data.Length+2);          // some ushort
        writer.WriteUShortLE((ushort)type);//Packet Type
        writer.WriteBytes(data);
        byte[] dataToSend = writer.ToBytes();
        //if(type != PacketType.Ping)
        //    _logger.Info($"Sending {type} [{string.Join(", ", dataToSend)}]");
        await Stream.WriteAsync(dataToSend, ct);
    }
}
