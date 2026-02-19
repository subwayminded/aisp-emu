using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets.Area;

public class AvatarNotifyMove(uint result, uint avatar_Id, MovementData moveData) : IPacket<AvatarNotifyMove>
{
    public uint Result = result;      // Обычно 1
    public uint AvatarId = avatar_Id; // Тот, кто двигается
    public MovementData Move = moveData;

    public static AvatarNotifyMove FromBytes(ReadOnlySpan<byte> data) => throw new NotImplementedException();

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Result);   // 4 байта
        writer.Write(AvatarId); // 4 байта
        writer.Write(Move.ToBytes()); // 14 байт
        return writer.ToBytes(); // Итого 22 байта
    }
}