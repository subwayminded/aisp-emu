
namespace AISpace.Common.Network.Packets;

public class AreasvEnterRequest : IPacket<AreasvEnterRequest>
{

    public uint UserID;
    public string OTP;
    public static AreasvEnterRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);
        AreasvEnterRequest req = new();
        req.UserID = reader.ReadUInt32LE();
        req.OTP = reader.ReadUtf8String(20);
        return req;
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
