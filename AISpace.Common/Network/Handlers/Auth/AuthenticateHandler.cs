using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Auth;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Auth;

public class AuthenticateHandler(IUserRepository repo, ILogger<AuthenticateHandler> logger) : IPacketHandler
{
    private readonly ILogger<AuthenticateHandler> _logger = logger;
    public PacketType RequestType => PacketType.AuthenticateRequest;
    public PacketType ResponseType => PacketType.AuthenticateResponse;

    public MessageDomain Domains => MessageDomain.Auth;


    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = AuthenticateRequest.FromBytes(payload.Span);
        _logger.LogInformation("Username: '{Username}', Password: {Password}", req.Username, req.Password);
        //TODO: Implement a check to repo
        bool valid = await repo.ValidateCredentialsAsync(req.Username, req.Password);
        uint userID = 31874;
        var AuthResp = new AuthenticateResponse(userID);
        await connection.SendAsync(ResponseType, AuthResp.ToBytes(), ct);
    }
}
