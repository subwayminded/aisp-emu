namespace AISpace.Common.Network.Packets;

public class AuthenticateRequest(string username, string password) : IPacket<AuthenticateRequest>
{
    public string Username = username;
    public string Password = password;

    public static AuthenticateRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        string username = reader.ReadNullTerminatedAscii();
        string password = reader.ReadNullTerminatedAscii();
        var packet = new AuthenticateRequest(username, password);
        return packet;
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
