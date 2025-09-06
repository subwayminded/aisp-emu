
namespace AISpace.Common.Network.Packets;

public class AvatarGetDataRequest : IPacket<AvatarGetDataRequest>
{
    public static AvatarGetDataRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
