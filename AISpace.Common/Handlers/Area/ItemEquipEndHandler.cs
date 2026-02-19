using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Area;

public class ItemEquipEndHandler(ILogger<ItemEquipEndHandler> logger) : IPacketHandler
{
    private readonly ILogger<ItemEquipEndHandler> _logger = logger;

    public PacketType RequestType => PacketType.ItemEquipEndRequest;
    // Мы не отвечаем ResponseType напрямую в базовом классе, делаем это вручную
    public PacketType ResponseType => PacketType.ItemEquipEndResponse; 
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var request = ItemEquipEndRequest.FromBytes(payload.Span);
        
        // _logger.LogInformation($"ItemEquipEnd ObjId: {request.ObjId}");

        // 1. Подтверждаем завершение (Result = 1 обычно означает успех в этом пакете)
        var response = new ItemEquipEndResponse(1);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);

        // 2. Отправляем уведомление, что экипировка завершена (чтобы разлочить UI)
        // Важно: используем ID из запроса или ID персонажа игрока
        uint charId = request.ObjId;
        if (connection.User != null && connection.User.Characters.Count > 0)
        {
             // Лучше использовать реальный ID персонажа, если он известен
             charId = (uint)connection.User.Characters.First().Id;
        }

        var equipEnded = new ItemEquipEnded(charId);
        await connection.SendAsync(PacketType.ItemEquipEnded, equipEnded.ToBytes(), ct);
    }
}