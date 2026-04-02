using AISpace.Common.DAL;
using AISpace.Common.Game;
using AISpace.Network;
using AISpace.Network.Packets.Area;
using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.Handlers.Area;

public class AreaItemGetListHandler(MainContext db) : IPacketHandler
{
    public PacketType RequestType => PacketType.ItemGetListRequest;
    public PacketType ResponseType => PacketType.ItemGetListResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, IPlayerSession session, CancellationToken ct = default)
    {
        var response = new ItemGetListResponse(0);
        await session.SendAsync(ResponseType, response.ToBytes(), ct);

        var chara = await db.Characters
            .Include(c => c.Inventory)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == session.CharacterId, ct);


        if (chara != null && chara.Inventory != null)
        {
            uint placeIndex = 0;
            foreach (var invItem in chara.Inventory)
            {
                var notify = new ItemUpdateListNotify(placeIndex, (uint)invItem.ItemId, (uint)invItem.ItemId);
                await session.SendAsync(PacketType.ItemUpdateListNotify, notify.ToBytes(), ct);
                
                placeIndex++;
            }
        }
    }
}
