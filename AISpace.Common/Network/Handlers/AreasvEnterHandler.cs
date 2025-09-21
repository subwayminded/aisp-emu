using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using NLog;

namespace AISpace.Common.Network.Handlers;

public class AreasvEnterHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.AreasvEnterRequest;

    public PacketType ResponseType => PacketType.AreasvEnterResponse;

    public MessageDomain Domains => MessageDomain.Area;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = AreasvEnterRequest.FromBytes(payload.Span);
        _logger.Info($"Client: {connection.Id} EnterRequest UserID: {req.UserID}, SessionID: {req.OTP}");

        var response = new AreasvEnterResponse(0, 25);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), ct);
                _logger.Info("Attempting to add Avatar");
                var charaData = new CharaData(24, 24, "randomuser");
                charaData.AddEquip(10100140, 0);
                charaData.AddEquip(10200130, 0);
                charaData.AddEquip(10100190, 0);
                var avatarData = new AvatarData(1, charaData);
                _ = new AvatarNotifyData(0, avatarData);
                await Task.Delay(TimeSpan.FromSeconds(1), ct);

                var moveData = new MovementData(-227.392f, -0.043f, -1418.097f, -119, MovementType.Stopped);
                var moveNotify = new AvatarNotifyMove(24, moveData);
                await connection.SendAsync(PacketType.AvatarNotifyMove, moveNotify.ToBytes(), ct);
            }
            catch (OperationCanceledException)
            {
            }
        }, ct);
    }
}
