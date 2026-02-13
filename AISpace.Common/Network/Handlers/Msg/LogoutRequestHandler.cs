using AISpace.Common.Network.Packets.Common;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class LogoutRequestHandler(ILogger<LogoutRequestHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.LogoutRequest;

    public PacketType ResponseType => PacketType.LogoutResponse;

    public MessageDomain Domain => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        logger.LogInformation("Logout requested from {ConnectionId}", connection.Id);
        await connection.SendAsync(ResponseType, new LogoutResponse().ToBytes(), ct);
    }
}
