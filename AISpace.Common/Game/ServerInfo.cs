namespace AISpace.Common.Game;

public class ServerInfo(string ip, ushort port)
{
    public ushort Port = port;
    public string IP = ip;

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.WriteUShortLE(Port);
        writer.WriteFixedAsciiString(IP, 65);
        return writer.ToBytes();
    }
}
