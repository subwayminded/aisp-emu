namespace AISpace.Common.Network.Packets.Avatar;

public class AvatarGetDataResponse(uint result) : IPacket<AvatarGetDataResponse>
{
    public static AvatarGetDataResponse FromBytes(ReadOnlySpan<byte> data)
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
