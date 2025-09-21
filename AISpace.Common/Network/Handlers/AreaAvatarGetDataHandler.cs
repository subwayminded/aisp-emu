using System;
using System.Threading;
using System.Threading.Tasks;
using AISpace.Common.Game;
using AISpace.Common.Network.Packets.Area;
using NLog;

namespace AISpace.Common.Network.Handlers;

public class AreaAvatarGetDataHandler : IPacketHandler
{
    public PacketType RequestType => PacketType.AvatarGetDataRequest;

    public PacketType ResponseType => PacketType.AvatarNotifyData;

    public MessageDomain Domains => MessageDomain.Area;

    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task HandleAsync(ReadOnlyMemory<byte> payload, ClientConnection connection, CancellationToken ct = default)
    {
        _logger.Info(BitConverter.ToString(payload.Span));
        var charaData = new CharaData(23, 23, "test");
        charaData.AddEquip(10100140, 0);
        charaData.AddEquip(10200130, 0);
        charaData.AddEquip(10100190, 0);
        var avatarData = new AvatarData(0, charaData);
        var notifyData = new AvatarNotifyData(0, avatarData);
        await connection.SendAsync(ResponseType, notifyData.ToBytes(), ct);
    }
}
