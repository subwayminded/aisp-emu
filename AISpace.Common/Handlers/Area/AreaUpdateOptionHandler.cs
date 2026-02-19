using AISpace.Common.Network.Packets;

namespace AISpace.Common.Network.Handlers;

public class AreaUpdateOptionHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.UpdateOptionRequest;
    public PacketType ResponseType => PacketType.UpdateOptionResponse;
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // Просто подтверждаем обновление опций
        var writer = new PacketWriter();
        writer.Write((uint)1); // Result 1 = OK
        await connection.SendAsync(ResponseType, writer.ToBytes(), ct);
    }
}