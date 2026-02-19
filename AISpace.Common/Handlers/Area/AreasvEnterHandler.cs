using AISpace.Common.DAL.Repositories;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreasvEnterHandler(IUserSessionRepository sessionRepo, ILogger<AreasvEnterHandler> _logger, SharedState sharedState) : IPacketHandler
{
    public PacketType RequestType => PacketType.AreasvEnterRequest;
    public PacketType ResponseType => PacketType.AreasvEnterResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = AreasvEnterRequest.FromBytes(payload.Span);
        var session = await sessionRepo.GetValidSessionAsync(req.OTP, ct);
        
        if (session == null) return;

        connection.User = session.User;
        connection.CharacterId = (uint)connection.User.Characters.First().Id;

        // ПРИНУДИТЕЛЬНЫЙ СБРОС КООРДИНАТ НА ЗЕМЛЮ
        connection.X = 0.0f;
        connection.Y = 0.0f;
        connection.Z = 0.0f;
        connection.Rotation = 0;

        sharedState.RegisterClient("Area", connection);

        _logger.LogInformation($"[AREA] {connection.User.Username} joined. Pos reset to ground.");

        var response = new AreasvEnterResponse(0, connection.CharacterId);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}