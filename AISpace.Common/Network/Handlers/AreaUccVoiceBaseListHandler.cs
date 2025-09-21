using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaUccVoiceBaseListHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.UccVoiceBaseListRequest;

    public PacketType ResponseType => PacketType.UccVoiceBaseListResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new UccVoiceBaseListResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
