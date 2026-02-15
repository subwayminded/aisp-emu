using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaGetMyAvatarMyprofileDataHandler(ILogger<AreaGetMyAvatarMyprofileDataHandler> logger) : IPacketHandler
{
    private readonly ILogger<AreaGetMyAvatarMyprofileDataHandler> _logger = logger;

    public PacketType RequestType => PacketType.GetMyAvatarMyprofileDataRequest;

    public PacketType ResponseType => PacketType.GetMyAvatarMyprofileDataResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new GetMyAvatarMyprofileDataResponse(0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
