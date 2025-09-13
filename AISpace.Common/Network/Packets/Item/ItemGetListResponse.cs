namespace AISpace.Common.Network.Packets.Item;

public class ItemGetListResponse(int Result) : IPacket<ItemGetListResponse>
{
    int result = Result;
    public static ItemGetListResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)result);//Result
        return writer.ToBytes();
    }
}
