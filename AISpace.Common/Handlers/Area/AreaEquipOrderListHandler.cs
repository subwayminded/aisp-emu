using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaEquipOrderListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.EquipOrderListRequest;
    public PacketType ResponseType => PacketType.EquipOrderListResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // Отправляем пустой список порядка (0), чтобы интерфейс не зависал
        var writer = new PacketWriter();
        writer.Write((uint)0); // Result
        writer.Write((uint)0); // Chara order count
        writer.Write((uint)0); // Job order count
        
        await connection.SendAsync(ResponseType, writer.ToBytes(), ct);
    }
}