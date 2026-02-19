using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaEmotionGetBaseListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.EmotionGetBaseListRequest;
    public PacketType ResponseType => PacketType.EmotionGetBaseListResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var writer = new PacketWriter();
        writer.Write((uint)0); // Result: 0 (Success)
        writer.Write((uint)0); // Count: 0 (Списка нет, используй встроенные)
        
        await connection.SendAsync(ResponseType, writer.ToBytes(), ct);
    }
}