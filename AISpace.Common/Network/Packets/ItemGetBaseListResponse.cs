using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets;

public class ItemGetBaseListResponse : IPacket<ItemGetBaseListResponse>
{

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
        List<byte[]> itemBytes = new();
        int length = 0;
        foreach (var item in Items)
        {
            byte[] temp = item.ToBytes();
            length += temp.Length;
            itemBytes.Add(temp);
        }
        length += 100;
        byte[] buffer = new byte[length];
        var payloadWriter = new PacketWriter(buffer);
        payloadWriter.WriteUInt32LE(0);//Result
        payloadWriter.WriteUInt32LE((uint)Items.Count);//Count of Items?
        foreach (var item in itemBytes)
            payloadWriter.WriteBytes(item);
        return payloadWriter.WrittenBytes;
    }
}
