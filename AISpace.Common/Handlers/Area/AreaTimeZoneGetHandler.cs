using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaTimeZoneGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.TimeZoneGetRequest;
    public PacketType ResponseType => PacketType.TimeZoneGetResponse;
    public MessageDomain Domain => MessageDomain.Area;

    private readonly ILogger<AreaTimeZoneGetHandler> _logger;

    public AreaTimeZoneGetHandler(ILogger<AreaTimeZoneGetHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // hand setup
        uint debugZone = 4; //early morning - 4, morning - 0, day - 1, evening - 2, night 3.
        float debugTime = 0f; // don't really understand how to make day-night scycle working properly
        byte debugFlag = 0;   

        _logger.LogInformation($"[TIME DEBUG] Zone: {debugZone} | Time: {debugTime}");

        var response = new TimeZoneGetResponse(0, debugZone, debugTime, 24, debugFlag);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}