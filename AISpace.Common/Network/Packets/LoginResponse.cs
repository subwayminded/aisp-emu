namespace AISpace.Common.Network.Packets;

public class LoginResponse : IPacket<LoginResponse>
{
    readonly uint result = 0;
    public static LoginResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        using var writer = new PacketWriter();
        writer.Write(result);//Result
        return writer.ToBytes();
    }
}
