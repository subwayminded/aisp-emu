using AISpace.Common.Network.Packets.Msg;
using NLog;

namespace AISpace.Common.Network.Handlers.Msg;

public class CmdExecHandler : IPacketHandler
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public PacketType RequestType => PacketType.CmdExecRequest;

    public PacketType ResponseType => PacketType.CmdExecResponse;

    public MessageDomain Domain => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var request = CmdExecRequest.FromBytes(payload.Span);
        string argsLog = request.Arguments.Count > 0 ? " args=[" + string.Join(", ", request.Arguments.Select(a => "\"" + a + "\"")) + "]" : "";
        _logger.Info($"CmdExec MID{request.MessageId} command=\"{request.Command}\" argCount={request.ArgCount}{argsLog}");

        var response = new CmdExecResponse(request.MessageId, result: 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
