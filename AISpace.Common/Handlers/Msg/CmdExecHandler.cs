using AISpace.Common.Network.Packets.Msg;
using AISpace.Common.Game;
using Microsoft.Extensions.Logging;

namespace AISpace.Common.Network.Handlers.Msg;

public class CmdExecHandler(SharedState state, ILogger<CmdExecHandler> logger) : IPacketHandler
{
    public PacketType RequestType => PacketType.CmdExecRequest;
    public PacketType ResponseType => PacketType.CmdExecResponse;
    public MessageDomain Domain => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var request = CmdExecRequest.FromBytes(payload.Span);
        
        // ВОЗВРАЩАЕМ ЛОГИ: теперь ты увидишь команду в консоли
        logger.LogInformation($"[CMD] Player {connection.CharacterId} used: /{request.Command} with {request.ArgCount} args");

        // 1. Стандартный ответ (ОБЯЗАТЕЛЬНО для стабильности клиента)
        var response = new CmdExecResponse(request.MessageId, 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);

        // 2. Обработка команд (без привязки к чату)
        string cmd = request.Command.ToLower();

        // Пример: команда /test - просто пишет в лог сервера
        if (cmd == "test") 
        {
            logger.LogInformation("Test command successful!");
        }
    }
}