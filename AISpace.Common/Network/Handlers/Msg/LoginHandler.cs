using System.Text;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Common;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class LoginHandler(IUserSessionRepository sessionRepo, ILogger<LoginHandler> logger) : IPacketHandler
{

    public PacketType RequestType => PacketType.LoginRequest;

    public PacketType ResponseType => PacketType.LoginResponse;

    public MessageDomain Domains => MessageDomain.Msg;

    private readonly IUserSessionRepository _sessionRepo = sessionRepo;
    private readonly ILogger<LoginHandler> _logger = logger;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var loginReq = LoginRequest.FromBytes(payload.Span);
        var otp = Encoding.ASCII.GetString(loginReq._otp);
        _logger.LogInformation("ListenerId: {ListenId} LoginRequest UserID: {UserID}, OTP: {OTP}", connection.Id, loginReq._userId, otp);
        var session = await _sessionRepo.GetValidSessionAsync(otp, ct);
        if (session is null || session.UserId != loginReq._userId)
        {
            _logger.LogWarning("Client: {ClientId} Login failed for UserID: {UserID} with OTP: {OTP}", connection.Id, loginReq._userId, otp);
            await connection.SendAsync(ResponseType, new LoginResponse(AuthResponseResult.InvalidCredentials).ToBytes(), ct);
            return;
        }

        //Set connection as authenticated
        connection.clientUser = session.User;
        _logger.LogInformation("Client: {ClientId} LoginRequest UserID: {UserID}, OTP: {OTP}, Name: {name}", connection.Id, loginReq._userId, otp, connection.clientUser.Username);
        await connection.SendAsync(ResponseType, new LoginResponse(AuthResponseResult.Success).ToBytes(), ct);
    }
}
