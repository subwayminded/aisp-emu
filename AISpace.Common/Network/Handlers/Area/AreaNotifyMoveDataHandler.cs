using AISpace.Common.Network.Packets.Area;
using NLog;

namespace AISpace.Common.Network.Handlers;

public class AreaNotifyMoveDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.NotifyMoveData;

    public PacketType ResponseType => PacketType.NotifyMoveData;

    public MessageDomain Domains => MessageDomain.Area;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var avatarMove = AvatarMove.FromBytes(payload.Span);
        var movement = avatarMove.Moves[0];
        _logger.Info($"X{movement.X:0.000} Y{movement.Y:0.000} Z{movement.Z:0.000} Yaw{movement.Rotation:000} D{(byte)movement.Animation:0}");
        return Task.CompletedTask;
    }
}
