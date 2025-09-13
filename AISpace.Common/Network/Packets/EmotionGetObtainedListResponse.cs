
namespace AISpace.Common.Network.Packets;

public class EmotionGetObtainedListResponse : IPacket<EmotionGetObtainedListResponse>
{
    public static EmotionGetObtainedListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0);//Result
        writer.Write((uint)0); // emotion_ids
        return writer.ToBytes();
    }
}
