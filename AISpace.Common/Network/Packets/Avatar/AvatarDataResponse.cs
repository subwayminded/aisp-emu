namespace AISpace.Common.Network.Packets.Avatar;

public class AvatarDataResponse(uint result, string name, uint modelId, uint islandID, uint slotId) : IPacket<AvatarDataResponse>
{
    public Game.CharaVisual Visual = new(1, 1, 1, 1, 2, 0, 0);
    public List<Game.ItemSlotInfo> Equips = new(30);

    //equips?

    public static AvatarDataResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public void AddEquip(uint id, uint socket)
    {
        Equips.Add(new Game.ItemSlotInfo(id, socket));
    }

    public byte[] ToBytes()
    {
        var _equips = Equips;
        while (_equips.Count < 30)
            AddEquip(0, 0);
        using var writer = new PacketWriter();
        writer.Write(result);
        writer.WriteNullTerminatedAsciiString(name);
        writer.Write(modelId);
        writer.Write(Visual.ToBytes());
        writer.Write(islandID, slotId);
        foreach (var equip in _equips)
            writer.WriteBytes(equip.ToBytes());
        return writer.Written.ToArray();
    }
}
