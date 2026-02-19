using AISpace.Common.Network.Packets;
using AISpace.Common.Network.Packets.Msg;
using NLog;

namespace AISpace.Common.Network.Handlers.Msg;

public class PostTalkHandler : IPacketHandler
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public PacketType RequestType => PacketType.PostTalkRequest;

    public PacketType ResponseType => PacketType.PostTalkResponse;

    public MessageDomain Domain => MessageDomain.Msg;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var chatMessage = PostTalkRequest.FromBytes(payload.Span);
        _logger.Info($"User says {chatMessage.Message} | MID{chatMessage.MessageID} | DID{chatMessage.DistID} | BID{chatMessage.BalloonID}");

        var response = new PostTalkResponse(chatMessage.MessageID, result: 0);
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
