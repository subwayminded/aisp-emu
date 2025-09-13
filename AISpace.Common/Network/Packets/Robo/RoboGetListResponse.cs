namespace AISpace.Common.Network.Packets.Robo;

public class RoboGetListResponse : IPacket<RoboGetListResponse>
{
    public static RoboGetListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0);//Result
        writer.Write((uint)0); // robo count
        return writer.ToBytes();
    }
}
