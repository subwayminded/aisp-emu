namespace AISpace.Common.Network.Packets.Area;

public class EmotionGetBaseListResponse : IPacket<EmotionGetBaseListResponse>
{
    public static EmotionGetBaseListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0);//Result
        writer.Write((uint)0);
        return writer.ToBytes();
    }
}
