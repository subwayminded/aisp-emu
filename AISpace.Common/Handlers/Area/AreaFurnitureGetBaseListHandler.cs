using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaFurnitureGetBaseListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.FurnitureGetBaseListRequest;

    public PacketType ResponseType => PacketType.FurnitureGetBaseListResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new FurnitureGetBaseListResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
