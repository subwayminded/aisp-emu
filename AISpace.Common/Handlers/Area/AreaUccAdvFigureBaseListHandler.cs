using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaUccAdvFigureBaseListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.UccAdvFigureBaseListRequest;

    public PacketType ResponseType => PacketType.UccAdvFigureBaseListResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new UccAdvFigureBaseListResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
