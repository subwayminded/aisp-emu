using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaEmotionGetObtainedListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.EmotionGetObtainedListRequest;

    public PacketType ResponseType => PacketType.EmotionGetObtainedListResponse;

    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new EmotionGetObtainedListResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
