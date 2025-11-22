namespace AISpace.Common.Network.Packets.Msg;

public class AvatarCreateResponse(uint Result) : IPacket<AvatarCreateResponse>
{
    public static AvatarCreateResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(Result);
        return writer.ToBytes();
    }
}
