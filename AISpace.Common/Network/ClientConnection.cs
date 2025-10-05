using System.Net;
using System.Net.Sockets;
using AISpace.Common.DAL.Entities;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AISpace.Common.Network;

public enum ClientState
{
    Init = 1,
    ConnectedToAuth = 2,
    ConnectedToMsg = 3,
    ConnectedToArea=4,
}

public class ClientConnection(Guid _Id, EndPoint _RemoteEndPoint, NetworkStream _ns, string _Version = "")
{
    private const byte HeaderPrefix = 0x03;
    private const int HeaderSize = 2;
    public CamelliaEngine Camellia = new();
    public KeyParameter? CamelliaKey;
    public ClientState CurrentState;
    public Guid Id = _Id;
    public EndPoint RemoteEndPoint = _RemoteEndPoint;
    public NetworkStream Stream = _ns;
    public string Version = _Version;
    public int connectedChannel = 0;
    public DateTimeOffset lastPing;

    public User? clientUser;
    public DateTimeOffset Connected { get; } = DateTimeOffset.UtcNow;

    public async Task SendRawAsync(byte[] data, CancellationToken ct = default) => await Stream.WriteAsync(data, ct);

    public void SetCamelliaKey(byte[] key)
    {
        CamelliaKey = new KeyParameter(key);
    }

    public async Task SendAsync(PacketType type, byte[] data, CancellationToken ct = default)
    {
        var writer = new PacketWriter();
        ushort packetType = (ushort)type;
        uint packetLength = (uint)data.Length + HeaderSize;
        writer.Write(HeaderPrefix);
        writer.Write(packetLength);
        writer.Write(packetType);
        writer.Write(data);
        byte[] dataToSend = writer.ToBytes();
        await SendRawAsync(dataToSend, ct);
    }

    public async Task SendAsync<T>(PacketType type, IPacket<T> packet, CancellationToken ct = default) where T : IPacket<T> => await SendAsync(type, packet.ToBytes(), ct);
}
