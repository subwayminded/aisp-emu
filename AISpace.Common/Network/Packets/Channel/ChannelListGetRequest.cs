namespace AISpace.Common.Network.Packets.Channel;

public class ChannelListGetRequest : IPacket<ChannelListGetRequest>
{
    public static ChannelListGetRequest FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
