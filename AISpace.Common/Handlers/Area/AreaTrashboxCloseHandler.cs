using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaTrashboxCloseHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.TrashboxCloseRequest;
    public PacketType ResponseType => PacketType.TrashboxCloseResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // 0 = Success. Это должно сказать клиенту "Ок, закрой окно".
        var response = new TrashboxCloseResponse(0); 
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}