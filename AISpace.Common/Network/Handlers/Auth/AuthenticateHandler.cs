using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Auth;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Auth;

public class AuthenticateHandler(IUserRepository userRepo, ILogger<AuthenticateHandler> logger) : PacketHandlerBase<AuthenticateRequest, AuthenticateResponse>
{
    private readonly ILogger<AuthenticateHandler> _logger = logger;

    public override PacketType RequestType => PacketType.AuthenticateRequest;
    public override PacketType ResponseType => PacketType.AuthenticateResponse;
    public override MessageDomain Domain => MessageDomain.Auth;

    public override async Task<AuthenticateResponse?> HandleAsync(AuthenticateRequest request, ClientConnection connection, CancellationToken ct = default)
    {
        _logger.LogInformation("Username: '{Username}'", request.Username);

        User? validUser = await userRepo.AuthenticateAsync(request.Username, request.Password);
        if (validUser is null)
        {
            _logger.LogWarning("Authentication failed for user '{Username}'", request.Username);
            var failResp = new AuthenticateFailureResponse(AuthResponseResult.InvalidCredentials);
            await connection.SendAsync(PacketType.AuthenticateFailureResponse, failResp.ToBytes(), ct);
            return null;
        }

        connection.User = validUser;
        return new AuthenticateResponse((uint)validUser.Id);
    }
}
