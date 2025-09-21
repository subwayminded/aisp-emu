namespace AISpace.Common.Network.Packets.Area;

public class AiDownloadListGetResponse : IPacket<AiDownloadListGetResponse>
{
    public static AiDownloadListGetResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0);//Result
        writer.Write((uint)0);//downs
        return writer.ToBytes();
    }
}
