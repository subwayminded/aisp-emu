namespace AISpace.Common.Network.Packets.Auth;

public class AuthenticateResponse(uint id) : IPacket<AuthenticateResponse>
{

    public static AuthenticateResponse FromBytes(ReadOnlySpan<byte> data)
    {

        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(id);
        return writer.ToBytes();
    }
}
