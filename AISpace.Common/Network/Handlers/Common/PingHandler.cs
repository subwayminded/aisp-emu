using AISpace.Common.Network.Packets.Common;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Common;

public class PingHandler(ILogger<PingHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.Ping;
    public PacketType ResponseType => PacketType.Ping;
    public MessageDomain Domains => MessageDomain.Msg | MessageDomain.Area | MessageDomain.Auth;

    ILogger<PingHandler> _logger = logger;
    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var ping = PingRequest.FromBytes(payload.Span);
        await connection.SendAsync(PacketType.Ping, ping.ToBytes(), ct);
    }
}
