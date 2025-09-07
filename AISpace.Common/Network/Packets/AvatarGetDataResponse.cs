
namespace AISpace.Common.Network.Packets;

public class AvatarGetDataResponse : IPacket<AvatarGetDataResponse>
{
    public static AvatarGetDataResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[4];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(0);
        return writer.Written.ToArray();
    }
}
