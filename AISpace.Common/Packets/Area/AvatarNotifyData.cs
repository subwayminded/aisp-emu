using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets.Area;

public class AvatarNotifyData(uint result, AvatarData avatarData) : IPacket<AvatarNotifyData>
{
    public static AvatarNotifyData FromBytes(ReadOnlySpan<byte> data) => throw new NotImplementedException();

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(result); // 4 байта (Обычно 0)
        writer.Write(avatarData.ToBytes()); // 928 байт
        return writer.ToBytes(); // Итого 932 байта
    }
}