namespace AISpace.Common.Game;

public class ChannelInfo(uint channelID, uint _0x0004, uint _0x0008, ServerInfo serverInfo)
{
    public uint channelID = channelID;

    public uint _0x0004 = _0x0004;
    public uint _0x0008 = _0x0008;
    public ServerInfo serverInfo = serverInfo;

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.WriteUIntLE(channelID);
        writer.WriteUIntLE(_0x0004);
        writer.WriteUIntLE(_0x0008);
        writer.WriteBytes(serverInfo.ToBytes());
        return writer.ToBytes();
    }
}
