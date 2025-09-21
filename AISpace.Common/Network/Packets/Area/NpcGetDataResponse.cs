namespace AISpace.Common.Network.Packets;

public class NpcGetDataResponse : IPacket<NpcGetDataResponse>
{
    public static NpcGetDataResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write((uint)0);//Result
        return writer.ToBytes();
    }
}
