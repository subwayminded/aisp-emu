namespace AISpace.Common.Network.Packets.Channel;

public class ChannelListGetResponse(uint result, List<Game.ChannelInfo> channels) : IPacket<ChannelListGetResponse>
{
    public uint Result = result;
    public List<Game.ChannelInfo> Channels = channels;

    public static ChannelListGetResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(Result);
        writer.Write((uint)Channels.Count);
        foreach (var channel in Channels)
            writer.Write(channel.ToBytes());
        return writer.ToBytes();
    }
}
