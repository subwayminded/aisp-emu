namespace AISpace.Common.Network.Packets.Avatar;

public class AvatarSelectRequest(uint slotId) : IPacket<AvatarSelectRequest>
{
    public uint SlotId = slotId;
    public static AvatarSelectRequest FromBytes(ReadOnlySpan<byte> data)
    {
        var reader = new PacketReader(data);
        uint slotId = reader.ReadUInt32LE();
        return new AvatarSelectRequest(slotId);
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }
}
