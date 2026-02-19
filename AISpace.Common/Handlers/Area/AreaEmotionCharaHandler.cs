using AISpace.Common.Network.Packets.Area;
using AISpace.Common.Game;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaEmotionCharaHandler(SharedState state, ILogger<AreaEmotionCharaHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.EmotionCharaRequest;
    public PacketType ResponseType => PacketType.EmotionCharaResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
{
    var req = EmotionCharaRequest.FromBytes(payload.Span);
    
    // ЛОГ: чтобы видеть, какой ID эмоции пришел
    logger.LogInformation($"[EMOTE] Player {connection.CharacterId} uses emote ID: {req.EmotionId}");

    // 1. Подтверждаем самому игроку (чтобы кнопка нажалась)
    await connection.SendAsync(ResponseType, new EmotionCharaResponse(connection.CharacterId, 0).ToBytes(), ct);

    // 2. Рассылка ВСЕМ игрокам в зоне (чтобы они увидели анимацию и услышали звук)
    var writer = new PacketWriter();
    writer.Write(connection.CharacterId); // КТО хлопает
    writer.Write(req.EmotionId);        // ЧТО делает

    byte[] notifyData = writer.ToBytes();

    foreach (var client in state.AreaClients.Values)
    {
        // Отправляем пакет 0x67B5 (NotifyEmotionChara)
        await client.SendAsync(PacketType.NotifyEmotionChara, notifyData, ct);
    }
  }
}