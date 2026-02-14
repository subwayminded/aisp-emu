namespace AISpace.Common.Network.Packets.Msg;

public class AvatarGetCreateInfoResponse : IPacket<AvatarGetCreateInfoResponse>
{
    private readonly List<uint> DefaultMaleBuilds = [1001011, 1001021, 1001031];
    private readonly List<byte> DefaultMaleFaces = [0, 1, 2, 3];
    private readonly List<uint> DefaultMaleHairStyles = [10920010, 10920020, 10920040];
    private readonly List<byte> DefaultMaleHairColours = [0, 4, 1, 2, 3];
    private readonly List<Game.ItemSlotInfo> DefaultMaleEquipment = [];

    private readonly List<uint> DefaultFemaleBuilds = [1002011, 1002021, 1002031];
    private readonly List<byte> DefaultFemaleFaces = [0, 1, 2, 3];
    private readonly List<uint> DefaultFemaleHairStyles = [10930010, 10930020, 10930040];
    private readonly List<byte> DefaultFemaleHairColours = [0, 4, 1, 2, 3];
    private readonly List<Game.ItemSlotInfo> DefaultFemaleEquipment = [];

    public static AvatarGetCreateInfoResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10100220, 0)); //Shirt
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10200100, 0)); //Pants
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10400030, 0)); //Socks
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10500070, 0)); //Shoes

        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10100060, 0)); //Shirt
        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10200090, 0)); //Shorts
        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10400000, 0)); //Socks
        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10500010, 0)); //Shoes
        var writer = new PacketWriter();

        //DefaultMaleBuilds
        writer.Write((uint)DefaultMaleBuilds.Count);
        foreach (var value in DefaultMaleBuilds)
            writer.Write(value);

        //DefaultMaleFaces
        writer.Write((uint)DefaultMaleFaces.Count);
        foreach (var value in DefaultMaleFaces)
            writer.Write(value);

        //DefaultMaleHairStyles
        writer.Write((uint)DefaultMaleHairStyles.Count);
        foreach (var value in DefaultMaleHairStyles)
            writer.Write(value);

        //DefaultMaleHairColours
        writer.Write((uint)DefaultMaleHairColours.Count);
        foreach (var value in DefaultMaleHairColours)
            writer.Write(value);

        //DefaultMaleEquipment
        writer.Write((uint)DefaultMaleEquipment.Count);
        foreach (var eq in DefaultMaleEquipment)
            writer.Write(eq.ToBytes());

        //DefaultFemaleBuilds
        writer.Write((uint)DefaultFemaleBuilds.Count);
        foreach (var value in DefaultFemaleBuilds)
            writer.Write(value);

        //DefaultFemaleFaces
        writer.Write((uint)DefaultFemaleFaces.Count);
        foreach (var value in DefaultFemaleFaces)
            writer.Write(value);

        //DefaultFemaleHairStyles
        writer.Write((uint)DefaultFemaleHairStyles.Count);
        foreach (var value in DefaultFemaleHairStyles)
            writer.Write(value);

        //DefaultFemaleHairColours
        writer.Write((uint)DefaultFemaleHairColours.Count);
        foreach (var value in DefaultFemaleHairColours)
            writer.Write(value);

        //DefaultFemaleEquipment
        writer.Write((uint)DefaultFemaleEquipment.Count);
        foreach (var eq in DefaultFemaleEquipment)
            writer.Write(eq.ToBytes());

        return writer.ToBytes();
    }
}
