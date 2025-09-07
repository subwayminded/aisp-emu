namespace AISpace.Common.Network.Packets;

public class VersionCheckResponse(uint major, uint minor, uint ver) : IPacket<VersionCheckResponse>
{
    readonly uint _result = 0;

    public static VersionCheckResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[16];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(_result); //4
        writer.WriteUInt32LE(major); //4
        writer.WriteUInt32LE(minor); //4
        writer.WriteUInt32LE(ver); //4
        return writer.WrittenBytes;
    }
}
