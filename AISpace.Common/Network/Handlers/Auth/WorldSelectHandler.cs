using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Auth;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Auth;

public class WorldSelectHandler(IUserRepository userRepo,IWorldRepository worldRepo, ILogger<WorldSelectHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.Auth_WorldSelectRequest;
    public PacketType ResponseType => PacketType.Auth_WorldSelectResponse;
    public MessageDomain Domains => MessageDomain.Auth;

    private readonly IWorldRepository _worldRepository = worldRepo;
    private readonly IUserRepository _userRepository = userRepo;
    private readonly ILogger<WorldSelectHandler> _logger = logger;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var WorldSelectReq = WorldSelectRequest.FromBytes(payload.Span);
        var selectedWorldID = (int)WorldSelectReq.WorldID;
        var world = await _worldRepository.GetWorldByIDAsync(selectedWorldID);
        if (world == null)//TODO: Should send a Logout notification?
            return;

        string otp = CryptoUtils.GenerateOTP();
        //Need to insert the otp into UserSessions
        //await _userRepository.AddSessionAsync(0, otp);
        _logger.LogInformation("World Selected: {ID}", selectedWorldID);
        var WorldSelectResp = new WorldSelectResponse(0, world.Address, world.Port, otp);
        await connection.SendAsync(PacketType.Auth_WorldSelectResponse, WorldSelectResp, ct);
    }
}
