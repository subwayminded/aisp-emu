namespace AISpace.Common.Network.Packets.Channel;

public class ChannelSelectRequest : IPacket<ChannelSelectRequest>
{
    public uint ChannelID;
    public static ChannelSelectRequest FromBytes(ReadOnlySpan<byte> data)
    {
        ChannelSelectRequest req = new();
        PacketReader reader = new(data);
        var channelID = reader.ReadUInt32LE();
        req.ChannelID = channelID;
        return req;
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
