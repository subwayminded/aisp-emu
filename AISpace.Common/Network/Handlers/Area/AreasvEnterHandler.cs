using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Area;
using AISpace.Common.Network.Packets.Common;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreasvEnterHandler(IUserSessionRepository sessionRepo, ILogger<AreasvEnterHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.AreasvEnterRequest;

    public PacketType ResponseType => PacketType.AreasvEnterResponse;

    public MessageDomain Domains => MessageDomain.Area;

    private readonly IUserSessionRepository _sessionRepo = sessionRepo;
    private readonly ILogger<AreasvEnterHandler> _logger = logger;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var loginReq = AreasvEnterRequest.FromBytes(payload.Span);
        _logger.LogInformation("Client: {Id} EnterRequest UserID: {UserID}, SessionID: {OTP}", connection.Id, loginReq.UserID, loginReq.OTP);
        var session = await _sessionRepo.GetValidSessionAsync(loginReq.OTP, ct);

        if (session is null || session.UserId != loginReq.UserID)
        {
            _logger.LogWarning("Client: {ClientId} Login failed for UserID: {UserID} with OTP: {OTP}", connection.Id, loginReq.UserID, loginReq.OTP);
            await connection.SendAsync(ResponseType, new LoginResponse(AuthResponseResult.InvalidCredentials).ToBytes(), ct);
            return;
        }

        connection.clientUser = session.User;
        uint charId = (uint)connection.clientUser.Characters.First().Id;
        _logger.LogInformation("Client: {ClientId} LoginRequest UserID: {UserID}, OTP: {OTP}, Name: {name}, CharID {charid}, CharName: {cname}", connection.Id, loginReq.UserID, loginReq.OTP, connection.clientUser.Username, charId, connection.clientUser.Characters.First().Name);

        var response = new AreasvEnterResponse(0, 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), ct);
                //_logger.Info("Attempting to add Avatar");
                //var charaData = new CharaData(24, 24, "randomuser");
                //charaData.AddEquip(10100140, 0);
                //charaData.AddEquip(10200130, 0);
                //charaData.AddEquip(10100190, 0);
                //var avatarData = new AvatarData(1, charaData);
                //_ = new AvatarNotifyData(0, avatarData);
                //await Task.Delay(TimeSpan.FromSeconds(1), ct);

                //var moveData = new MovementData(-227.392f, -0.043f, -1418.097f, -119, MovementType.Stopped);
                //var moveNotify = new AvatarNotifyMove(0, (uint)connection.clientUser.Characters.First().Id, moveData);
                //await connection.SendAsync(PacketType.AvatarNotifyMove, moveNotify.ToBytes(), ct);
            }
            catch (OperationCanceledException)
            {
            }
        }, ct);
    }
}
