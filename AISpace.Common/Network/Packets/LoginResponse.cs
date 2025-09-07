namespace AISpace.Common.Network.Packets;

public class LoginResponse : IPacket<LoginResponse>
{
    public static LoginResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[4];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(0);//Result
        return writer.WrittenBytes;
    }
}
