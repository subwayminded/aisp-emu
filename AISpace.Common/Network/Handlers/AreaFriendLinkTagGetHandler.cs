using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaFriendLinkTagGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.FriendLinkTagGetRequest;

    public PacketType ResponseType => PacketType.FriendLinkTagGetResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new FriendLinkTagGetResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
