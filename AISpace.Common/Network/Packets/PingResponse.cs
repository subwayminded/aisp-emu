namespace AISpace.Common.Network.Packets;

public class PingResponse(uint _time) : IPacket<PingResponse>
{
    public uint time = _time;
    public static PingResponse FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint currentTime = reader.ReadUInt32LE();
        return new PingResponse(currentTime);
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[4];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(time);//Result
        return writer.WrittenBytes;
    }
}
