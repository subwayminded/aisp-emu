namespace AISpace.Common.Network.Packets.Robo;

public class RoboGetListRequest : IPacket<RoboGetListRequest>
{
    public static RoboGetListRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
