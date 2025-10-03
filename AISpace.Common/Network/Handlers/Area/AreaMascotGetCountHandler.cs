using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaMascotGetCountHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.MascotGetCountRequest;

    public PacketType ResponseType => PacketType.MascotGetCountResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new MascotGetCountResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
