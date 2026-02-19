using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaFriendGetListDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.FriendGetListDataRequest;

    public PacketType ResponseType => PacketType.FriendGetListDataResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new FriendGetListDataResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
