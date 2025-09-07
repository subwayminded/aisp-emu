namespace AISpace.Common.Game;

public class ItemSlotInfo(uint id, uint socket)
{
    public uint Id = id;
    public uint Socket = socket;

    public byte[] ToBytes()
    {
        Span<byte> buffer = stackalloc byte[8];
        var writer = new Network.PacketWriter(buffer);
        writer.WriteUInt32LE(Id);
        writer.WriteUInt32LE(Socket);
        return writer.WrittenBytes;
    }
}
