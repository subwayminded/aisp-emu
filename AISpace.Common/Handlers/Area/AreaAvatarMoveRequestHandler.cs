using AISpace.Common.Network.Packets.Area;
using AISpace.Common.Game;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Area;

public class AreaAvatarMoveRequestHandler(ILogger<AreaAvatarMoveRequestHandler> _logger, SharedState state) : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarMoveRequest;
    public PacketType ResponseType => PacketType.AvatarNotifyMove;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var moveReq = AvatarMove.FromBytes(payload.Span);
        if (moveReq.Moves.Length == 0) return;
        var lastPoint = moveReq.Moves[^1];

        // ВЫЧИСЛЯЕМ БЕГ: Если дистанция большая - ставим Running
        float dist = (float)Math.Sqrt(Math.Pow(lastPoint.X - connection.X, 2) + Math.Pow(lastPoint.Z - connection.Z, 2));
        if (dist > 0.0f) lastPoint.Animation = MovementType.Running;
        else if (dist > 0.0f) lastPoint.Animation = MovementType.Walking;

        connection.X = lastPoint.X; connection.Y = lastPoint.Y; connection.Z = lastPoint.Z;
        connection.Rotation = lastPoint.Rotation;

        // Рассылка через 0xAADB
        var notify = new AvatarNotifyMove(1, connection.CharacterId, lastPoint).ToBytes();
        foreach (var other in state.AreaClients.Values)
        {
            if (other.Id == connection.Id) continue;
            await other.SendAsync(PacketType.AvatarNotifyMove, notify, ct);
        }
    }
}