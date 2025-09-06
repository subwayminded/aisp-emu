namespace AISpace.Common.Network.Packets;

public class LoginResponse : IPacket<LoginResponse>
{
    private readonly uint _length = 6;
    public static LoginResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[(int)(_length + 5)];
        var writer = new PacketWriter(buffer);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE(_length);
        writer.WriteUInt16LE((ushort)PacketType.LoginResponse);//Packet Type
        writer.WriteUInt32LE(0);//Result
        return writer.WrittenBytes;
    }
}
