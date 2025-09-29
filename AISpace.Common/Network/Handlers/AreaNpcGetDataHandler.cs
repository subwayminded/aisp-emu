using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaNpcGetDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.NpcGetDataRequest;

    public PacketType ResponseType => PacketType.NpcGetDataResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new NpcGetDataResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
