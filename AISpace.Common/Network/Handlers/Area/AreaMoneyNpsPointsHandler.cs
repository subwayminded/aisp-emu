using AISpace.Common.Network.Packets.Area;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers;

public class AreaMoneyNpsPointsHandler(ILogger<AreaMoneyNpsPointsHandler> logger) : IPacketHandler
{
    private readonly ILogger<AreaMoneyNpsPointsHandler> _logger = logger;
    public PacketType RequestType => PacketType.MoneyNpsPointsRequest;
    public PacketType ResponseType => PacketType.MoneyNpsPointsResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // Request has 0 bytes. Response: result (4), total (8), limit (8) = 20 bytes. recv_nps_point_get_r
        var response = new MoneyNpsPointsResponse(0, 120, 9999); // 0 = success, total/limit placeholder
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
