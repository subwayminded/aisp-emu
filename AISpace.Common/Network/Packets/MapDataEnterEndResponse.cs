
namespace AISpace.Common.Network.Packets;

public class MapDataEnterEndResponse : IPacket<MapDataEnterEndResponse>
{
    public static MapDataEnterEndResponse FromBytes(ReadOnlySpan<byte> data)
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