using AISpace.Common.Network;

namespace AISpace.Common.Network.Packets.Area;

public class NotifyEmotionChara(uint objId, uint emotionId) : IPacket<NotifyEmotionChara>
{
    public uint ObjId = objId;
    public uint EmotionId = emotionId;

    public static NotifyEmotionChara FromBytes(ReadOnlySpan<byte> data)
    {
        var reader = new PacketReader(data);
        return new NotifyEmotionChara(reader.ReadUInt(), reader.ReadUInt());
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(ObjId);
        writer.Write(EmotionId);
        return writer.ToBytes();
    }
}