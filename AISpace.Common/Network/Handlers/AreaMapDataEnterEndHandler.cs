using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaMapDataEnterEndHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.MapDataEnterEndRequest;

    public PacketType ResponseType => PacketType.MapDataEnterEndResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new MapDataEnterEndResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
