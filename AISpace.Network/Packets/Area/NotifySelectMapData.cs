using AISpace.Network;

namespace AISpace.Network.Packets.Area;

public class NotifySelectMapData : IPacket<NotifySelectMapData>
{
    public IReadOnlyList<uint> MapIds { get; set; } = [];

    public NotifySelectMapData() { }

    public NotifySelectMapData(uint singleMapId)
    {
        MapIds = [singleMapId];
    }

    public NotifySelectMapData(IEnumerable<uint> mapIds)
    {
        MapIds = mapIds.ToList();
    }

    public static NotifySelectMapData FromBytes(ReadOnlySpan<byte> data) => throw new NotImplementedException("Server does not receive this packet");

    public byte[] ToBytes()
    {
        const int SelectMapEntrySizeInPacket = 109;
        var writer = new PacketWriter();
        writer.Write((uint)MapIds.Count);
        
        Span<byte> padding = stackalloc byte[SelectMapEntrySizeInPacket - 4];
        padding.Clear();

        foreach (var mapId in MapIds)
        {
            writer.Write(mapId);
            writer.Write(padding);
        }
        return writer.ToBytes();
    }
}
