using AISpace.Common.DAL.Entities;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network.Packets.Auth;

namespace AISpace.Common.Network.Handlers.Auth;

public class WorldListHandler(IWorldRepository repo) : IPacketHandler
{
    public PacketType RequestType => PacketType.Auth_WorldListRequest;
    public PacketType ResponseType => PacketType.Auth_WorldListResponse;
    public MessageDomain Domains => MessageDomain.Auth;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        //var worlds = await repo.GetAllWorldsAsync();
        var worlds = new List<World>();
        var world = new World();
        world.Description = "test";
        world.Name = "test2";
        world.Id = 0;
        worlds.Add(world);
        var worldListResponse = new WorldListResponse(0, worlds);
        await connection.SendAsync(PacketType.Auth_WorldListResponse, worldListResponse.ToBytes(), ct);
    }
}
