namespace AISpace.Common.Game;

public class ItemSlotInfo(uint id, uint socket)
{
    public uint Id = id;
    public uint Socket = socket;

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.WriteUIntLE(Id);
        writer.WriteUIntLE(Socket);
        return writer.ToBytes();
    }
}
