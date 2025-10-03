using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaRoboVoiceTypeUpdateHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.RoboVoiceTypeUpdateRequest;

    public PacketType ResponseType => PacketType.RoboVoiceTypeUpdateResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new RoboVoiceTypeUpdateResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
