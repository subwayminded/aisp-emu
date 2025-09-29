using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaMapEnterHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.MapEnterRequest;

    public PacketType ResponseType => PacketType.MapEnterResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new MapEnterResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
