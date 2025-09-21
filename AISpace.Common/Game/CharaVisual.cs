using AISpace.Common.Network;

namespace AISpace.Common.Game;

public class CharaVisual(uint bloodType, byte month, byte day, uint gender, uint characterID, byte face, uint hairstyle)
{
    public uint BloodType = bloodType;
    public byte Month = month;
    public byte Day = day;
    public uint Gender = gender;
    public uint CharacterID = characterID;
    public byte Face = face;
    public uint Hairstyle = hairstyle;

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(BloodType);
        writer.Write(Month);
        writer.Write(Day);
        writer.Write(Gender);
        writer.Write(CharacterID);
        writer.Write(Face);
        writer.Write(Hairstyle);
        return writer.ToBytes();
    }
}
