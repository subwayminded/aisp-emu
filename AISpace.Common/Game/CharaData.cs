namespace AISpace.Common.Game;

public class CharaData(uint chara_id, uint character_id, string name)
{
    public uint chara_id = chara_id; //Possibly Character Slot ID?
    public uint character_id = character_id; //Possibly Avatar ID
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
        uint y = 0;
        while (Equips.Count < 30)
            AddEquip(0, y++);

        var writer = new Network.PacketWriter();
        writer.Write(chara_id);
        writer.Write(character_id);
        writer.WriteFixedString(name, 37, "SHIFT_JIS"); //37
        writer.Write(Visual.ToBytes());
        writer.Write(0f); //Quaternion X
        writer.Write(0f); //Quaternion Y
        writer.Write(0f); //Quaternion Z
        writer.Write(0f); //Quaternion W
        //writer.Write(moveData.ToBytes());
        writer.Write(moveData.ToBytes());
        writer.Write(new byte[4]);
        writer.Write(new byte[8]);
        writer.Write((ushort)30);
        for (int i = 0; i < 30; i++)
        {
            writer.Write(Equips[i].ToBytes());
        }
        writer.Write(new byte[1]);
        return writer.ToBytes();
    }
}
