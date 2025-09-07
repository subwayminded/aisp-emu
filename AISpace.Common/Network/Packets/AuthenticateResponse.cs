namespace AISpace.Common.Network.Packets;

public class AuthenticateResponse(uint id) : IPacket<AuthenticateResponse>
{
    public uint ID = id;

    public static AuthenticateResponse FromBytes(ReadOnlySpan<byte> data)
    {

        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[1000];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(ID);
        return writer.Written.ToArray();
    }
}
