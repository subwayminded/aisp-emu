using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaTrashboxCloseHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.TrashboxCloseRequest;
    public PacketType ResponseType => PacketType.TrashboxCloseResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new TrashboxCloseResponse(1);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
