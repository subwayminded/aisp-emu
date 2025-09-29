using AISpace.Common.Game;
namespace AISpace.Common.Network.Packets.Area;

public class AvatarMove(MovementData[] Moves) : IPacket<AvatarMove>
{
    public MovementData[] Moves = Moves;
    public static AvatarMove FromBytes(ReadOnlySpan<byte> data)
    {
        var packetReader = new PacketReader(data);
        ushort MoveCount = 2;
        MovementData[] movement = new MovementData[MoveCount];
        for (int i = 0; i < MoveCount; i++)
            movement[i] = MovementData.FromBytes(packetReader.ReadBytes(14));
        AvatarMove avatarMove = new(movement);
        return avatarMove;
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
