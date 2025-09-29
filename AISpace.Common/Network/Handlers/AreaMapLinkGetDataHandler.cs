using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaMapLinkGetDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.MapLinkGetDataRequest;

    public PacketType ResponseType => PacketType.MapLinkGetDataResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new MapLinkGetDataResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
