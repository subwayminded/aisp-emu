namespace AISpace.Common.Network.Packets;

public class LoginRequest(uint userid, byte[] otp) : IPacket<LoginRequest>
{
    public uint UserID = userid;
    public byte[] OTP = otp;
    public static LoginRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint userid = reader.ReadUInt32LE();
        byte[] otp = reader.ReadBytes(20).ToArray();
        return new LoginRequest(userid, otp);
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
