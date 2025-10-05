using AISpace.Common.Network.Packets.Area;
using NLog;

namespace AISpace.Common.Network.Handlers.Area;

public class AreaNotifyMoveDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.NotifyMoveData;

    public PacketType ResponseType => PacketType.NotifyMoveData;

    public MessageDomain Domains => MessageDomain.Area;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var avatarMove = AvatarMove.FromBytes(payload.Span);
        var movement = avatarMove.Moves[0];
        _logger.Info($"X{movement.X:0} Y{movement.Y:0} Z{movement.Z:0} Rot{movement.Rotation:000} A{(byte)movement.Animation:0}");
    }
}
