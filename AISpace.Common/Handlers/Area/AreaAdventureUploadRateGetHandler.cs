using AISpace.Common.Game;
using AISpace.Network;
using AISpace.Network.Packets.Area;

namespace AISpace.Common.Handlers.Area;

public class AreaAdventureUploadRateGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.AdventureUploadRateGetRequest;
    public PacketType ResponseType => PacketType.AdventureUploadRateGetResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, IPlayerSession session, CancellationToken ct = default)
    {
        var writer = new PacketWriter();
        writer.Write((uint)1); 
        await session.SendAsync(ResponseType, writer.ToBytes(), ct);
    }
}