using AISpace.Common.Network.Packets.Area;
using AISpace.Common.Game;

namespace AISpace.Common.Network.Handlers;

public class AreaEmotionCharaHandler(SharedState state) : IPacketHandler
{
    public PacketType RequestType => PacketType.EmotionCharaRequest;
    public PacketType ResponseType => PacketType.EmotionCharaResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var request = EmotionCharaRequest.FromBytes(payload.Span);
        
        // 1. Ответ (чтобы кнопка в интерфейсе "отжалась")
        var response = new EmotionCharaResponse(connection.CharacterId, 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);

        // 2. Уведомление ВСЕМ (включая себя, чтобы проигрался звук/анимация)
        var notify = new NotifyEmotionChara(connection.CharacterId, request.EmotionId);
        byte[] notifyData = notify.ToBytes();

        foreach (var other in state.AreaClients.Values)
        {
            await other.SendAsync(PacketType.NotifyEmotionChara, notifyData, ct);
        }
    }
}