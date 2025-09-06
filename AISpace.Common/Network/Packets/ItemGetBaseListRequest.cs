namespace AISpace.Common.Network.Packets;

public class ItemGetBaseListRequest : IPacket<ItemGetBaseListRequest>
{
    public static ItemGetBaseListRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
