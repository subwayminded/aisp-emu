namespace AISpace.Common.Network.Packets;

public class PingRequest(uint Time) : IPacket<PingRequest>
{
    public static PingRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint currentTime = reader.ReadUInt32LE();
        return new PingRequest(currentTime);
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(Time);//Result
        return writer.ToBytes();
    }
}
