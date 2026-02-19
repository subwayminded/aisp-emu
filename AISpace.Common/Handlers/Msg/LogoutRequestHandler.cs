using AISpace.Common.Network.Packets.Common;
using AISpace.Common.Game;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class LogoutRequestHandler(ILogger<LogoutRequestHandler> logger, SharedState state) : IPacketHandler
{
    public PacketType RequestType => PacketType.LogoutRequest;
    public PacketType ResponseType => PacketType.LogoutResponse;
    public MessageDomain Domain => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        logger.LogInformation($"[LOGOUT] {connection.User?.Username} is leaving.");

        // 1. Отвечаем клиенту
        await connection.SendAsync(ResponseType, new LogoutResponse().ToBytes(), ct);

        // 2. Ждем полсекунды и закрываем стрим, чтобы сработал Cleanup в TcpListenerService
        await Task.Delay(500);
        connection.Stream.Close(); 
    }
}