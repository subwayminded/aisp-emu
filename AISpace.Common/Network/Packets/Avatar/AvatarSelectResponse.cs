namespace AISpace.Common.Network.Packets.Avatar;

public class AvatarSelectResponse(uint result) : IPacket<AvatarSelectResponse>
{
    public static AvatarSelectResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(result);
        return writer.ToBytes();
    }
}
