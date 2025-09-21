using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Auth;

namespace AISpace.Common.Network.Handlers;

public class WorldListHandler(IWorldRepository repo) : IPacketHandler
{
    public PacketType RequestType => PacketType.Auth_WorldListRequest;
    public PacketType ResponseType => PacketType.Auth_WorldListResponse;
    public MessageDomain Domains => MessageDomain.Auth;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var worlds = await repo.GetAllWorldsAsync();
        var worldListResponse = new WorldListResponse(0, worlds);
        await connection.SendAsync(PacketType.Auth_WorldListResponse, worldListResponse.ToBytes(), ct);
    }
}
