using AISpace.Network;

namespace AISpace.Network.Packets.Area;

public class ItemUpdateListNotify(uint place, uint serialId, uint targetId) : IPacket<ItemUpdateListNotify>
{
    public uint Place = place;
    public uint SerialId = serialId;
    public uint TargetId = targetId;

    public static ItemUpdateListNotify FromBytes(ReadOnlySpan<byte> data)
    {
        var reader = new PacketReader(data);
        return new ItemUpdateListNotify(reader.ReadUInt(), reader.ReadUInt(), reader.ReadUInt());
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Place);
        writer.Write(SerialId);
        writer.Write(TargetId);
        return writer.ToBytes();
    }
}