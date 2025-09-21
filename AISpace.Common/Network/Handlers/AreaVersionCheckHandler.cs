using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Network.Packets.Common;
using NLog;

namespace AISpace.Common.Network.Handlers;

public class AreaVersionCheckHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.Msg_VersionCheckRequest;

    public PacketType ResponseType => PacketType.Msg_VersionCheckResponse;

    public MessageDomain Domains => MessageDomain.Area;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = VersionCheckRequest.FromBytes(payload.Span);
        _logger.Info($"Client: {connection.Id} Version: {connection.Version}");
        var resp = new VersionCheckResponse(0, req.Major, req.Minor, req.Version);
        await connection.SendAsync(ResponseType, resp.ToBytes(), ct);
    }
}
