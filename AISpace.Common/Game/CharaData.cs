namespace AISpace.Common.Game;

public class CharaData(uint chara_id, uint character_id, string name)
{
    public uint chara_id = chara_id; 
    public uint character_id = character_id;
    public string name = name;
    public CharaVisual Visual = new(BloodType.A, 1, 1, 1, 2, 0, 0);
    public MovementData moveData = new(0, 0, 0, 0, 0);
    public List<Game.ItemSlotInfo> Equips = new(30);

    public void AddEquip(uint id, uint socket)
    {
        if (Equips.Count < 30) Equips.Add(new ItemSlotInfo(id, socket));
    }

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.Write(chara_id);
        writer.Write(character_id);
        writer.WriteFixedString(name, 37, "SHIFT_JIS");
        writer.Write(Visual.ToBytes()); // В этом блоке передается пол (Gender)
        
        writer.Write(0f); writer.Write(0f); writer.Write(0f); writer.Write(1f); // Rotation
        writer.Write(moveData.ToBytes()); // Position (14 bytes)
        writer.Write(new byte[12]); // Padding

        writer.Write((ushort)30); // Количество слотов
        for (int i = 0; i < 30; i++)
        {
            // Если в списке Equips меньше 30 элементов, добиваем нулями
            if (i < Equips.Count) writer.Write(Equips[i].ToBytes());
            else { writer.Write((uint)0); writer.Write((uint)i); }
        }
        writer.Write((byte)0);
        return writer.ToBytes();
    }
}