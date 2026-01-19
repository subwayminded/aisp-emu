namespace AISpace.Common.Game;

public class AvatarData(uint avatar_id, CharaData chara)
{
    public readonly uint avatar_id = avatar_id;
    public readonly CharaData chara = chara;

    public byte[] ToBytes()
    {
        Network.PacketWriter writer = new();
        writer.Write(avatar_id);
        writer.Write(chara.ToBytes());
        writer.Write((ushort)8);
        writer.Write(new byte[573]);
        return writer.ToBytes();
    }
}
