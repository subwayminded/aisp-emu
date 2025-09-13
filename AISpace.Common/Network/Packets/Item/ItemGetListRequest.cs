namespace AISpace.Common.Network.Packets.Item;

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
