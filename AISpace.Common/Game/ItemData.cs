using AISpace.Common.Network;

namespace AISpace.Common.Game;

public class ItemData
{
    // used as key in item manager
    public uint Key { get; set; } = 0;

    // used as priority in sorted list
    public uint SortedListPriority { get; set; } = 0;

    // used for item in data
    public uint ItemId { get; set; } = 0;

    // used in skill manager
    public uint SkillId { get; set; } = 0;

    public string Name { get; set; } = "N/A";

    // seems like rest is item types, 20 is skill?
    // [0-11] = ?
    // [12-14] = ?
    // [15-16] = ?
    // [17] = ?
    // [20] = skill
    public uint Category { get; set; } = 0;

    public uint Socket1 { get; set; } = 0; // PartSocket
    public uint Socket2 { get; set; } = 0; // PartSocket

    public string Description { get; set; } = "N/A";
    public string LimitDesc { get; set; } = "N/A";

    // flags (0x2 = non tradable)
    public uint Flags { get; set; } = 0;

    public ushort _0x0448 { get; set; } = 0;
    public uint _0x044c { get; set; } = 0; // used as key in some map
    public uint _0x0450 { get; set; } = 0;
    public uint EmotionId { get; set; } = 0;
    public uint _0x0458 { get; set; } = 0;


    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.WriteUIntLE(Key);
        writer.WriteUIntLE(SortedListPriority);
        writer.WriteUIntLE(ItemId);
        writer.WriteUIntLE(SkillId);
        writer.WriteFixedJisString(Name, 97);
        writer.WriteUIntLE(Category);
        writer.WriteUIntLE(Socket1);
        writer.WriteUIntLE(Socket2);
        writer.WriteFixedJisString(Description, 769);
        writer.WriteFixedJisString(LimitDesc, 193);
        writer.WriteUIntLE(Flags);
        writer.WriteUShortLE(_0x0448);
        writer.WriteUIntLE(_0x044c);
        writer.WriteUIntLE(_0x0450);
        writer.WriteUIntLE(EmotionId);
        writer.WriteUIntLE(_0x0458);

        byte[] data = writer.ToBytes().AsSpan(0, writer.Length).ToArray();
        return data;
    }
}
