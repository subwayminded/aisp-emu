using AISpace.Common.Game;
using System.Numerics;
namespace AISpace.Common.Network.Packets.Avatar;

public class AvatarMove(uint Result, MoveData[] Moves) : IPacket<AvatarMove>
{
    public uint Result = Result;
    public MoveData[] Moves = Moves;
    public static AvatarMove FromBytes(ReadOnlySpan<byte> data)
    {
        var packetReader = new PacketReader(data);
        //uint result = packetReader.ReadUInt32LE(); //4
        ushort MoveCount = 2;
        MoveData[] moves = new MoveData[MoveCount];
        //for (int i = 0;i <= MoveCount; i++)
        //{
        MoveData dat = MoveData.FromBytes(packetReader.ReadBytes(14));
        int offset = packetReader.Position;
        MoveData dat2 = MoveData.FromBytes(packetReader.ReadBytes(14));
        //float x = packetReader.ReadFloat(); //4
        //float y = packetReader.ReadFloat(); //4
        //float z = packetReader.ReadFloat(); //4
        //sbyte yaw = packetReader.ReadSByte(); //1
        //byte d = packetReader.ReadByte(); //1
        //MoveData dat = new(new Vector3(x, y, z), yaw, d);
        //18    
        moves[0] = dat;
        moves[1] = dat;
        //}
        AvatarMove avatarMove = new(0, moves);
        return avatarMove;
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
