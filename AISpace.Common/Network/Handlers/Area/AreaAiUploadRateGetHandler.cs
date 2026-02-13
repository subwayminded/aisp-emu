using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaAiUploadRateGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.AiUploadRateGetRequest;

    public PacketType ResponseType => PacketType.AiUploadRateGetResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new AiUploadRateGetResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
