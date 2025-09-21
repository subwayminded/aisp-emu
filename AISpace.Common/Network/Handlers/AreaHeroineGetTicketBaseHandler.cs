using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaHeroineGetTicketBaseHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.HeroineGetTicketBaseRequest;

    public PacketType ResponseType => PacketType.HeroineGetTicketBaseResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new HeroineGetTicketBaseResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
