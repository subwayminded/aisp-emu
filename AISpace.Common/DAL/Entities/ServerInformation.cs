namespace AISpace.Common.DAL.Entities;

public class ServerInformation(string ip, ushort port)
{
    public ushort Port = port;
    public string IP = ip;
}
