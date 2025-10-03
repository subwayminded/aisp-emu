using AISpace.Common.Network.Packets.Common;

namespace AISpace.Common.Network.Handlers.Common;

public class PingHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.Ping;
    public PacketType ResponseType => PacketType.Ping;
    public MessageDomain Domains => MessageDomain.Msg | MessageDomain.Area | MessageDomain.Auth;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var ping = PingRequest.FromBytes(payload.Span);
        await connection.SendAsync(PacketType.Ping, ping.ToBytes(), ct);
    }
}
