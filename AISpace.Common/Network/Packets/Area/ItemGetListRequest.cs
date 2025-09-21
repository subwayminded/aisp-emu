namespace AISpace.Common.Network.Packets.Area;

public class ItemGetListRequest : IPacket<ItemGetListRequest>
{
    public static ItemGetListRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
