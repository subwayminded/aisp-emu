namespace AISpace.Common.Network.Packets.Area;

public class EmotionGetBaseListResponse(uint Result = 0) : IPacket<EmotionGetBaseListResponse>
{
    public static EmotionGetBaseListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Result);
        writer.Write((uint)0); //Array length?
        return writer.ToBytes();
    }
}
