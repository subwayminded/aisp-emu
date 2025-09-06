namespace AISpace.Common.Network.Packets;

public class PingResponse(uint _time) : IPacket<PingResponse>
{
    private readonly uint _length = 4;
    public uint time = _time;
    public static PingResponse FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint currentTime = reader.ReadUInt32LE();
        return new PingResponse(currentTime);
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[(int)(_length + 5)];
        var writer = new PacketWriter(buffer);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE(_length);
        writer.WriteUInt16LE((ushort)PacketType.PingResponse);//Packet Type
        writer.WriteUInt32LE(time);//Result
        return writer.WrittenBytes;
    }
}
