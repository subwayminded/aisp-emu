using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaMapDataEnterEndHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.MapDataEnterEndRequest; // 0x04B4
    public PacketType ResponseType => PacketType.MapDataEnterEndResponse; // 0xBE02
    public MessageDomain Domain => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        // Подтверждаем, что мы знаем: игрок догрузился.
        var writer = new PacketWriter();
        writer.Write((uint)0); // Result: 0
        
        await connection.SendAsync(ResponseType, writer.ToBytes(), ct);
        
        Console.WriteLine($"[MAP] Player {connection.CharacterId} finished loading resources.");
    }
}