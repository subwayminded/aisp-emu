
namespace AISpace.Common.Network.Packets;

public class NiconiCommonsBaseListRequest : IPacket<NiconiCommonsBaseListRequest>
{
    public static NiconiCommonsBaseListRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
