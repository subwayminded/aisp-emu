
using AISpace.Common.Game;

namespace AISpace.Common.Network.Packets;

public class AvatarDataResponse(uint result, string name, uint modelId, uint islandID, uint slotId) : IPacket<AvatarDataResponse>
{
    public uint Result = result;
    public string Name = name;
    public uint ModelId = modelId;
    
    public uint RegIslandID = islandID;
    public uint SlotId = slotId;
    public CharaVisual Visual = new CharaVisual(1,1,1,1,2,0,0);
    public List<ItemSlotInfo> Equips = new List<ItemSlotInfo>(30);

    //equips?

    public static AvatarDataResponse FromBytes(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    public byte[] ToBytes()
    {

        Span<byte> buffer = stackalloc byte[1000];
        var writer = new PacketWriter(buffer);
        writer.WriteUInt32LE(Result);
        writer.WriteNullTerminatedAsciiString(name);
        writer.WriteUInt32LE(modelId);
        writer.WriteBytes(Visual.ToBytes());
        writer.WriteUInt32LE(RegIslandID);
        writer.WriteUInt32LE(slotId);
        foreach (var equip in Equips)
            writer.WriteBytes(equip.ToBytes());
        return writer.Written.ToArray();
    }
}
