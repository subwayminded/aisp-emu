using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaEmotionGetObtainedListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.EmotionGetObtainedListRequest;
    public PacketType ResponseType => PacketType.EmotionGetObtainedListResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var writer = new PacketWriter();
        writer.Write((uint)0); // Result: 0 (Success)
        writer.Write((uint)0); // Count: 0 
        
        await connection.SendAsync(ResponseType, writer.ToBytes(), ct);
    }
}