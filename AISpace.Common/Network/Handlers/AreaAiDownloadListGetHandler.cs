using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Network.Packets.Area;

namespace AISpace.Common.Network.Handlers;

public class AreaAiDownloadListGetHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.AiDownloadListGetRequest;

    public PacketType ResponseType => PacketType.AiDownloadListGetResponse;

    public MessageDomain Domains => MessageDomain.Area;

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        var response = new AiDownloadListGetResponse();
        await connection.SendAsync(ResponseType, response.ToBytes(), ct);
    }
}
