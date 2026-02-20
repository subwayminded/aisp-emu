using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaUpdateOptionHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.UpdateOptionRequest;

    public PacketType ResponseType => PacketType.UpdateOptionResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new UpdateOptionResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
