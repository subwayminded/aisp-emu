namespace AISpace.Common.Network.Packets;

public class VersionCheckResponse(uint major, uint minor, uint ver) : IPacket<VersionCheckResponse>
{
    readonly byte _length = 18;
    readonly uint _result = 0;

    public static VersionCheckResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[_length + 5];
        var writer = new PacketWriter(buffer);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE(_length);          // some ushort
        writer.WriteUInt16LE((ushort)PacketType.VersionCheckResponse); //2
        writer.WriteUInt32LE(_result); //4
        writer.WriteUInt32LE(major); //4
        writer.WriteUInt32LE(minor); //4
        writer.WriteUInt32LE(ver); //4
        return writer.WrittenBytes;
    }
}
