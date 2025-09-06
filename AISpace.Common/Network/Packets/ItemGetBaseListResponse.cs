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
        //1114 per item
        uint _length = (uint)(new ItemData().ToBytes().Length * Items.Count) + 10;
        byte[] buffer = new byte[_length + 5];
        var payloadWriter = new PacketWriter(buffer);
        //writer.WriteByte(0x03);
        //writer.WriteUInt32LE(_length);
        payloadWriter.WriteUInt16LE((ushort)PacketType.ItemGetBaseListResponse);//Packet Type
        payloadWriter.WriteUInt32LE(0);//Result
        payloadWriter.WriteUInt32LE((uint)Items.Count);//Count of Items?
        foreach (var item in Items)
        {
            payloadWriter.WriteBytes(item.ToBytes());
        }
        byte[] full = payloadWriter.WrittenBytes.AsSpan(0, payloadWriter.Length).ToArray();
        byte[] buffer2 = new byte[_length + 5];

        var writer = new PacketWriter(buffer2);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE((uint)full.Length);
        writer.WriteBytes(full);
        return writer.WrittenBytes.AsSpan(0, writer.Length).ToArray();
    }
}
