using AISpace.Common.Network.Packets.Common;

namespace AISpace.Common.Network.Handlers;

public class VersionCheckHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.Auth_VersionCheckRequest;
    public PacketType ResponseType => PacketType.Auth_VersionCheckResponse;
    public MessageDomain Domains => MessageDomain.Msg | MessageDomain.Area | MessageDomain.Auth;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var req = VersionCheckRequest.FromBytes(payload.Span);
        //TODO: Implement some way of checking version is 'correct'
        var resp = new VersionCheckResponse(0, req.Major, req.Minor, req.Version);

        //Just send back the same version as the client sent
        await connection.SendAsync(ResponseType, resp.ToBytes(), ct);
    }
}
