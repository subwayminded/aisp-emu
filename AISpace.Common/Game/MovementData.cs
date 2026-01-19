namespace AISpace.Common.Game;

public enum MovementType : byte
{
    Stopped = 0,
    Walking = 1,
    Running = 2,
}
public class MovementData(float x, float y, float z, sbyte rotation, MovementType animation)
{

    public float X = x;
    public float Y = y;
    public float Z = z;
    public sbyte Rotation = rotation;
    public MovementType Animation = animation;

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Z);
        writer.Write(Rotation);
        writer.Write((byte)Animation);
        return writer.ToBytes();
    }

    public static MovementData FromBytes(ReadOnlySpan<byte> source)
    {
        var reader = new Network.PacketReader(source);
        var x = reader.ReadFloat();
        var y = reader.ReadFloat();
        var z = reader.ReadFloat();
        var rotation = reader.ReadSByte();
        var movementType = (MovementType)reader.ReadByte();
        var moveData = new MovementData(x, y, z, rotation, movementType);
        return moveData;

    }
}
