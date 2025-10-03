using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaMissionDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.MissionDataRequest;

    public PacketType ResponseType => PacketType.MissionDataResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new MissionDataResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
