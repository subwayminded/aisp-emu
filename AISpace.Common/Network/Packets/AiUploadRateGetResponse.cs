
namespace AISpace.Common.Network.Packets;

public class AiUploadRateGetResponse : IPacket<AiUploadRateGetResponse>
{
    public static AiUploadRateGetResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)1);//Result
        return writer.ToBytes();
    }
}
