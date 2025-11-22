using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Auth;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Auth;

public class AuthenticateHandler(IUserRepository userRepo, ILogger<AuthenticateHandler> logger) : IPacketHandler
{
    private readonly ILogger<AuthenticateHandler> _logger = logger;
    public PacketType RequestType => PacketType.AuthenticateRequest;
    public PacketType ResponseType => PacketType.AuthenticateResponse;

    public MessageDomain Domains => MessageDomain.Auth;


    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = AuthenticateRequest.FromBytes(payload.Span);
        _logger.LogInformation("Username: '{Username}'", req.Username);

        User? validUser = await userRepo.AuthenticateAsync(req.Username, req.Password);
        if(validUser is null)
        {
            _logger.LogWarning("Authentication failed for user '{Username}'", req.Username);
            var failResp = new AuthenticateFailureResponse(AuthResponseResult.InvalidCredentials);
            await connection.SendAsync(PacketType.AuthenticateFailureResponse, failResp.ToBytes(), ct);
            return;
        }
        connection.clientUser = validUser;
        var AuthResp = new AuthenticateResponse((uint)validUser.Id);
        await connection.SendAsync(ResponseType, AuthResp.ToBytes(), ct);
    }
}
