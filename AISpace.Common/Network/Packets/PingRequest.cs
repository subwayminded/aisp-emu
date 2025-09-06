namespace AISpace.Common.Network.Packets;

public class PingRequest(uint _time) : IPacket<PingRequest>
{
    private readonly uint _length = 6;
    public uint time = _time;
    public static PingRequest FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint currentTime = reader.ReadUInt32LE();
        return new PingRequest(currentTime);
    }

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[(int)(_length + 5)];
        var writer = new PacketWriter(buffer);
        writer.WriteByte(0x03);
        writer.WriteUInt32LE(_length);
        writer.WriteUInt16LE(0xC202);//Packet Type
        writer.WriteUInt32LE(time);//Result
        return writer.WrittenBytes;
    }
}
