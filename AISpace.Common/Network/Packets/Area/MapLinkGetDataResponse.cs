namespace AISpace.Common.Network.Packets.Area;

public class MapLinkGetDataResponse : IPacket<MapLinkGetDataResponse>
{
    public static MapLinkGetDataResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0); //Result
        return writer.ToBytes();
    }
}
