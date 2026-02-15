using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaEmotionCharaHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.EmotionCharaRequest;
    public PacketType ResponseType => PacketType.EmotionCharaResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var request = EmotionCharaRequest.FromBytes(payload.Span);
        var response = new EmotionCharaResponse(request.ObjId, 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
        var notify = new NotifyEmotionChara(request.ObjId, request.EmotionId);
        await connection.SendAsync(PacketType.NotifyEmotionChara, notify.ToBytes(), ct);
    }
}
