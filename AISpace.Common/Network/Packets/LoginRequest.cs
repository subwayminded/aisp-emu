namespace AISpace.Common.Network.Packets;

public class LoginRequest(uint userid, ReadOnlySpan<byte> otp) : IPacket<LoginRequest>
{
    public uint UserID = userid;
    public byte[] OTP = otp.ToArray();
    public static LoginRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint userid = reader.ReadUInt32LE();
        ReadOnlySpan<byte> otp = reader.ReadBytes(20);
        return new LoginRequest(userid, otp);
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
