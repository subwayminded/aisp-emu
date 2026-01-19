namespace AISpace.Common.Game;

public class CharaData(uint chara_id, uint character_id, string name)
{

    public uint chara_id = chara_id;//Possibly Character Slot ID?
    public uint character_id = character_id;//Possibly Avatar ID
    public string name = name;
    public CharaVisual Visual = new(BloodType.A, 1, 1, 1, 2, 0, 0);
    public MovementData moveData = new(0, 0, 0, 0, 0);
    //X-4069.790 Y-0.043 Z-2813.927
    public List<Game.ItemSlotInfo> Equips = new(30);

    public void AddEquip(uint id, uint socket)
    {
        Equips.Add(new ItemSlotInfo(id, socket));
    }
    public byte[] ToBytes()
    {
        while (Equips.Count < 30)
            AddEquip(0, 0);
        var writer = new Network.PacketWriter();
        writer.Write(chara_id);
        writer.Write(character_id);
        writer.WriteFixedString(name, 37, "ASCII");//37
        writer.Write(Visual.ToBytes());
        writer.Write(moveData.ToBytes());
        writer.Write(moveData.ToBytes());
        writer.Write(new byte[6]);
        writer.Write(new byte[8]);
        writer.Write((ushort)Equips.Count);
        foreach (var item in Equips)
            writer.Write(item.ToBytes());
        writer.Write(new byte[1]);
        return writer.ToBytes();
    }
}
