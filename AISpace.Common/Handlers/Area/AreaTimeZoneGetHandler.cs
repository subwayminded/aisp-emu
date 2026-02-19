using AISpace.Common.Network.Packets;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaTimeZoneGetHandler(ILogger<AreaTimeZoneGetHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.TimeZoneGetRequest;
    public PacketType ResponseType => PacketType.TimeZoneGetResponse;
    public MessageDomain Domain => MessageDomain.Area;

    private readonly ILogger<AreaTimeZoneGetHandler> _logger = logger;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // 1. Берем реальное время (UTC) и переводим в Японское (JST, UTC+9)
        var japanTime = DateTime.UtcNow.AddHours(9);
        int hour = japanTime.Hour;

        // 2. Определяем зону (Скайбокс)
        // 0: Утро (05:00 - 09:59)
        // 1: День (10:00 - 15:59)
        // 2: Вечер (16:00 - 18:59)
        // 3: Ночь (19:00 - 04:59)
        uint timezoneId = 3;

        if (hour >= 5 && hour < 10) timezoneId = 0;
        else if (hour >= 10 && hour < 16) timezoneId = 1;
        else if (hour >= 16 && hour < 19) timezoneId = 2;
        else timezoneId = 3;

        // 3. Формируем ответ
        // TimeZoneMax = 24 (длительность цикла)
        // Flag = 1 (ВАЖНО: Принудительное обновление скайбокса без интерполяции)
        var response = new TimeZoneGetResponse(0, timezoneId, (uint)hour, 24, 1);
        
        // _logger.LogInformation($"Time Sync: {hour}:00 (Zone {timezoneId})");

        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}