namespace AISpace.Common.Game;

public class AvatarData(uint Result, CharaData chara)
{
    public readonly uint Result = Result;
    public readonly CharaData chara = chara;

    public byte[] ToBytes()
    {
        Network.PacketWriter writer = new();
        writer.Write(Result);
        writer.Write(chara.ToBytes());
        writer.Write((ushort)8);
        writer.Write(new byte[573]);
        return writer.ToBytes(); //928 bytes
    }
}
