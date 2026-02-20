using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets.Area;

public class AvatarMove(MovementData[] Moves) : IPacket<AvatarMove>
{
    public MovementData[] Moves = Moves;

    public static AvatarMove FromBytes(ReadOnlySpan<byte> data)
    {
        var reader = new PacketReader(data);
        int count = data.Length / 14; if (count == 0) count = 1;
        var moves = new MovementData[count];
        for (int i = 0; i < count; i++) moves[i] = MovementData.FromBytes(reader.ReadBytes(14));
        return new AvatarMove(moves);
    }

    public byte[] ToBytes() => throw new NotImplementedException();
}