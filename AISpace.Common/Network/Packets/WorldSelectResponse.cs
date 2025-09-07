namespace AISpace.Common.Network.Packets;

public class WorldSelectResponse(string ipAddress, ushort port) : IPacket<WorldSelectResponse>
{
    readonly byte _length = 97;

    public string ipAddress = ipAddress;
    public ushort port = port;

    public static WorldSelectResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[_length];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(0);//Result
        writer.WriteUInt32LE(1);//Count of worlds?
        writer.WriteUInt16LE(port);
        writer.WriteFixedAsciiString(ipAddress, 65);
        writer.WriteFixedAsciiString("", 20);
        return writer.WrittenBytes;
    }
}
