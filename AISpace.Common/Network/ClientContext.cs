using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;

namespace AISpace.Common.Network;

public class ClientContext(Guid _Id, EndPoint _RemoteEndPoint, NetworkStream _ns, string _Version = "")
{
    public Guid Id = _Id;
    public EndPoint RemoteEndPoint = _RemoteEndPoint;
    public NetworkStream Stream = _ns;
    public string Version = _Version;

    public async Task SendAsync(ushort type, byte[] payload, CancellationToken ct = default)
    {
        int length = 2 + (payload?.Length ?? 0);
        byte[] buffer = new byte[1 + length];

        buffer[0] = (byte)length;
        BinaryPrimitives.WriteUInt16BigEndian(buffer.AsSpan(1, 2), type);

        if (payload is { Length: > 0 })
            payload.CopyTo(buffer, 3);

        await Stream.WriteAsync(buffer, ct);
    }

    public async Task SendAsync(byte[] data, CancellationToken ct = default)
    {
        await Stream.WriteAsync(data, ct);
    }

    public async Task SendAsync(PacketType type, byte[] data, CancellationToken ct = default)
    {
        byte[] buffer = new byte[data.Length+7];
        PacketWriter writer = new PacketWriter(buffer);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE((uint)data.Length+2);          // some ushort
        writer.WriteUInt16LE((ushort)type);//Packet Type
        writer.WriteBytes(data);
        byte[] dataToSend = writer.Written.ToArray();
        await Stream.WriteAsync(dataToSend, ct);
    }
}
