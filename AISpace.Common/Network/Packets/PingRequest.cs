namespace AISpace.Common.Network.Packets;

public class PingRequest(uint _time) : IPacket<PingRequest>
{
    public uint time = _time;
    public static PingRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint currentTime = reader.ReadUInt32LE();
        return new PingRequest(currentTime);
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[(int)(4)];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(time);//Result
        return writer.WrittenBytes;
    }
}
