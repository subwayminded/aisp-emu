using System.Text;
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
        Encoding shiftJis = Encoding.GetEncoding("Shift_JIS");
        Span<byte> buffer = stackalloc byte[2000];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(Key);
        writer.WriteUInt32LE(SortedListPriority);
        writer.WriteUInt32LE(ItemId);
        writer.WriteUInt32LE(SkillId);
        writer.WriteFixedJisString(Name, 97);
        writer.WriteUInt32LE(Category);
        writer.WriteUInt32LE(Socket1);
        writer.WriteUInt32LE(Socket2);
        writer.WriteFixedJisString(Description, 769);
        writer.WriteFixedJisString(LimitDesc, 193);
        writer.WriteUInt32LE(Flags);
        writer.WriteUInt16LE(_0x0448);
        writer.WriteUInt32LE(_0x044c);
        writer.WriteUInt32LE(_0x0450);
        writer.WriteUInt32LE(EmotionId);
        writer.WriteUInt32LE(_0x0458);

        byte[] data = writer.WrittenBytes.AsSpan(0, writer.Length).ToArray();
        return data;
    }
}
