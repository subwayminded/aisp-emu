namespace AISpace.Common.Network.Packets.Area;

public class MapEnterResponse : IPacket<MapEnterResponse>
{
    public static MapEnterResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0);//Result
        return writer.ToBytes();
    }
}

