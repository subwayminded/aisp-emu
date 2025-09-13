using AISpace.Common.Network;

namespace AISpace.Common.Game;

public class CharaVisual(uint bloodType, byte month, byte day, uint gender, uint charaID, byte face, uint hairstyle)
{
    public uint BloodType = bloodType;
    public byte Month = month;
    public byte Day = day;
    public uint Gender = gender;
    public uint CharaID = charaID;
    public byte Face = face;
    public uint Hairstyle = hairstyle;

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.WriteUIntLE(BloodType);
        writer.WriteByte(Month);
        writer.WriteByte(Day);
        writer.WriteUIntLE(Gender);
        writer.WriteUIntLE(CharaID);
        writer.WriteByte(Face);
        writer.WriteUIntLE(Hairstyle);
        return writer.ToBytes();
    }
}
