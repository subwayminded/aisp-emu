namespace AISpace.Common.Network.Packets.Area;

public class AreasvEnterRequest : IPacket<AreasvEnterRequest>
{

    public uint UserID;
    public string OTP;
    public static AreasvEnterRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);
        AreasvEnterRequest req = new()
        {
            UserID = reader.ReadUInt(),
            OTP = reader.ReadFixedString(20)
        };
        return req;
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
