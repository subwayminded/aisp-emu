using AISpace.Common.Network.Packets.Common;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class LogoutRequestHandler(ILogger<AvatarCreateHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.LogoutRequest;

    public PacketType ResponseType => PacketType.LogoutResponse;

    public MessageDomain Domains => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        await connection.SendAsync(ResponseType, new LogoutResponse().ToBytes(), ct);
    }
}
