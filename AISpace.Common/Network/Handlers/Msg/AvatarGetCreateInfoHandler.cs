using AISpace.Common.Network.Packets.Msg;

namespace AISpace.Common.Network.Handlers.Msg;

public class AvatarGetCreateInfoHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarGetCreateInfoRequest;

    public PacketType ResponseType => PacketType.AvatarGetCreateInfoResponse;

    public MessageDomain Domains => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        AvatarGetCreateInfoResponse resp = new();
        await connection.SendAsync(ResponseType, resp);
    }
}
