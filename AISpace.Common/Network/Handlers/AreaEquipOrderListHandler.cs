using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaEquipOrderListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.EquipOrderListRequest;

    public PacketType ResponseType => PacketType.EquipOrderListResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new EquipOrderListResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
