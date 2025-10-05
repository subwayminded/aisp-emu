namespace AISpace.Common.Network.Packets.Msg;

public class AvatarGetCreateInfoResponse : IPacket<AvatarGetCreateInfoResponse>
{

    private readonly List<uint> DefaultMaleBuilds = [1001011, 1001021, 1001031];
    private readonly List<byte> DefaultMaleFaces = [0, 1, 2, 3];
    private readonly List<uint> DefaultMaleHairStyles = [10920010, 10920020, 10920040];
    private readonly List<byte> DefaultMaleHairColours = [ 0, 4, 1, 2, 3 ];
    private readonly List<Game.ItemSlotInfo> DefaultMaleEquipment = [];


    private readonly List<uint> DefaultFemaleBuilds = [1002011, 1002021, 1002031];
    private readonly List<byte> DefaultFemaleFaces = [0, 1, 2, 3];
    private readonly List<uint> DefaultFemaleHairStyles = [10930010, 10930020, 10930040];
    private readonly List<byte> DefaultFemaleHairColours = [0, 4, 1, 2, 3];
    private readonly List<Game.ItemSlotInfo> DefaultFemaleEquipment = [];



    //equips?

    public static AvatarGetCreateInfoResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }
    //Avatar Create Request
    //43-68-61-72-6B-69-00-1B-4A-0F-00-04-00-00-00-01-01-02-00-00-00-01-00-00-00-03-7A-C7-A6-00-00-00-00-00

    public byte[] ToBytes()
    {
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10100220, 0));//Shirt
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10200100, 0));//Pants
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10400030, 0));//Socks
        DefaultMaleEquipment.Add(new Game.ItemSlotInfo(10500070, 0));//Shoes

        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10100060, 0));//Shirt
        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10200090, 0));//Shorts
        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10400000, 0));//Socks
        DefaultFemaleEquipment.Add(new Game.ItemSlotInfo(10500010, 0));//Shoes
        var writer = new PacketWriter();

        writer.Write((uint)DefaultMaleBuilds.Count);
        writer.Write(DefaultMaleBuilds.ToArray());

        writer.Write((uint)DefaultMaleFaces.Count);
        writer.Write(DefaultMaleFaces.ToArray());

        writer.Write((uint)DefaultMaleHairStyles.Count);
        writer.Write(DefaultMaleHairStyles.ToArray());

        writer.Write((uint)DefaultMaleHairColours.Count);
        writer.Write(DefaultMaleHairColours.ToArray());

        writer.Write((uint)DefaultMaleEquipment.Count);

        foreach (var eq in DefaultMaleEquipment)
            writer.Write(eq.ToBytes());

        writer.Write((uint)DefaultFemaleBuilds.Count);
        writer.Write(DefaultFemaleBuilds.ToArray());

        writer.Write((uint)DefaultFemaleFaces.Count);
        writer.Write(DefaultFemaleFaces.ToArray());

        writer.Write((uint)DefaultFemaleHairStyles.Count);
        writer.Write(DefaultFemaleHairStyles.ToArray());

        writer.Write((uint)DefaultFemaleHairColours.Count);
        writer.Write(DefaultFemaleHairColours.ToArray());

        writer.Write((uint)DefaultFemaleEquipment.Count);
        foreach (var eq in DefaultFemaleEquipment)
            writer.Write(eq.ToBytes());

        /*var _equips = Equips;
        while (_equips.Count < 30)
            AddEquip(0, 0);
        
        
        writer.Write(name);
        writer.Write(modelId);
        writer.Write(Visual.ToBytes());
        writer.Write(islandID, slotId);
        foreach (var equip in _equips)
           writer.Write(equip.ToBytes());*/
        return writer.ToBytes();
    }
}
