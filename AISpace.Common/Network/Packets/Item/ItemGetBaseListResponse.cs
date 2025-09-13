using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets.Item;

public class ItemGetBaseListResponse : IPacket<ItemGetBaseListResponse>
{
    uint result = 0;
    readonly List<ItemData> Items;

    public ItemGetBaseListResponse()
    {
        Items = [];
        foreach (var line in File.ReadLines("testitems.csv"))
        {
            var parts = line.Split(',');
            if (parts.Length >= 3)
            {
                var temp = new ItemData
                {
                    Key = uint.Parse(parts[0]),
                    SortedListPriority = uint.Parse(parts[0]),
                    ItemId = uint.Parse(parts[0]),
                    Socket1 = uint.Parse(parts[1]),
                    Socket2 = uint.Parse(parts[1]),
                    Name = parts[2]
                };
                Items.Add(temp);
            }
        }
        //Get Item List
    }
    public static ItemGetBaseListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(result, (uint)Items.Count);
        foreach (var item in Items)
            writer.Write(item.ToBytes());
        return writer.ToBytes();
    }
}
