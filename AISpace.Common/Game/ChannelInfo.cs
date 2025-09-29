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
        writer.Write(channelID);
        writer.Write(_0x0004);
        writer.Write(_0x0008);
        writer.Write(serverInfo.ToBytes());
        return writer.ToBytes();
    }
}
