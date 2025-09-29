namespace AISpace.Common.Network.Packets.Area;

public class EmotionGetObtainedListResponse(uint Result = 0) : IPacket<EmotionGetObtainedListResponse>
{
    public static EmotionGetObtainedListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Result);//Result
        writer.Write((uint)0); // emotion_ids
        return writer.ToBytes();
    }
}
