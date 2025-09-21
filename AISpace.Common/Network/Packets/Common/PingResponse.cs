namespace AISpace.Common.Network.Packets.Common;

public class PingResponse(uint _time) : IPacket<PingResponse>
{
    public uint time = _time;
    public static PingResponse FromBytes(ReadOnlySpan<byte> data)
    {
        PacketReader reader = new(data);

        uint currentTime = reader.ReadUInt();
        return new PingResponse(currentTime);
    }

    public byte[] ToBytes()
    {
        var writer = new PacketWriter();
        writer.Write(time);//Result
        return writer.ToBytes();
    }
}
