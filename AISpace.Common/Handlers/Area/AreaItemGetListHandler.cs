using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaItemGetListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.ItemGetListRequest;

    public PacketType ResponseType => PacketType.ItemGetListResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new ItemGetListResponse(0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
