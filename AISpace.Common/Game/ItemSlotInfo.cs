namespace AISpace.Common.Game;

public class ItemSlotInfo(uint id, uint socket)
{
    public uint Id = id;//4
    public uint Socket = socket;//4

    public byte[] ToBytes()
    {
        var writer = new Network.PacketWriter();
        writer.Write(Id);
        writer.Write(Socket);
        return writer.ToBytes();
    }
}