using AISpace.Common.Network.Packets.Auth;
using NLog;

namespace AISpace.Common.Network.Handlers.Auth;

public class WorldSelectHandler() : IPacketHandler
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public PacketType RequestType => PacketType.Auth_WorldSelectRequest;
    public PacketType ResponseType => PacketType.Auth_WorldSelectResponse;
    public MessageDomain Domains => MessageDomain.Auth;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var WorldSelectReq = WorldSelectRequest.FromBytes(payload.Span);
        var selectedWorldID = WorldSelectReq.WorldID;
        string otp = CryptoUtils.GenerateOTP();
        //Need to insert the otp into UserSessions
        _logger.Info($"World Selected: {selectedWorldID}");
        var WorldSelectResp = new WorldSelectResponse(0, "127.0.0.1", 50052, otp);
        await connection.SendAsync(PacketType.Auth_WorldSelectResponse, WorldSelectResp.ToBytes(), ct);
    }
}
