using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaTimeZoneGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.TimeZoneGetRequest;

    public PacketType ResponseType => PacketType.TimeZoneGetResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new TimeZoneGetResponse(0, 1, 0, 8, 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
