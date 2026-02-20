using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;


namespace AISpace.Common.Network.Handlers;

public class AreaMapEnterHandler(ILogger<AreaMapEnterHandler> logger) : IPacketHandler
{
    private readonly ILogger<AreaMapEnterHandler> _logger = logger;

    public PacketType RequestType => PacketType.MapEnterRequest;
    public PacketType ResponseType => PacketType.MapEnterResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        _logger.LogInformation("Client {Id} requested MapEnter (Minimal Mode)", connection.Id);

        // Отправляем просто "Успех" (4 байта)
        var response = new AreaMapEnterResponse(0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}