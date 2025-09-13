namespace AISpace.Common.Network.Packets.Emotion;

public class EmotionGetBaseListRequest : IPacket<EmotionGetBaseListRequest>
{
    public static EmotionGetBaseListRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
