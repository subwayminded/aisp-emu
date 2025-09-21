using System;
using System.Collections.Generic;
using System.Linq;
using AISpace.Common.Network;
using AISpace.Common.Network.Handlers;
using NLog;

namespace AISpace.Area.Server;

public class AreaServer(int port = 50054)
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private static readonly IReadOnlyList<IPacketHandler> _handlers = new IPacketHandler[]
    {
        new PingHandler(),
        new AreaVersionCheckHandler(),
        new AreasvEnterHandler(),
        new AreaAvatarGetDataHandler(),
        new AreaRoboGetListHandler(),
        new AreaItemGetListHandler(),
        new AreaEquipOrderListHandler(),
        new AreaFurnitureGetBaseListHandler(),
        new AreaEmotionGetBaseListHandler(),
        new AreaUccAdvFigureBaseListHandler(),
        new AreaUccVoiceBaseListHandler(),
        new AreaNiconiCommonsBaseListHandler(),
        new AreaMissionDataHandler(),
        new AreaMapDataEnterEndHandler(),
        new AreaFriendLinkTagGetHandler(),
        new AreaFriendGetListDataHandler(),
        new AreaEmotionGetObtainedListHandler(),
        new AreaHeroineGetTicketBaseHandler(),
        new AreaNpcGetDataHandler(),
        new AreaMapLinkGetDataHandler(),
        new AreaMyRoomGetFurnitureHandler(),
        new AreaTimeZoneGetHandler(),
        new AreaMascotGetCountHandler(),
        new AreaMoneyDataGetHandler(),
        new AreaAiDownloadListGetHandler(),
        new AreaAiUploadRateGetHandler(),
        new AreaUpdateOptionHandler(),
        new AreasvLeaveHandler(),
        new AreaRoboVoiceTypeUpdateHandler(),
        new AreaMapEnterHandler(),
        new AreaNotifyMoveDataHandler(),
    };

    private readonly TcpListenerService tcpServer = new("0.0.0.0", port, false);

    public async void Start()
    {
        _logger.Info("Starting Area server");
        tcpServer.Start();
        await foreach (var packet in tcpServer.PacketReader.ReadAllAsync())
        {
            var handler = _handlers.FirstOrDefault(h => h.RequestType == packet.Type && h.Domains.HasFlag(MessageDomain.Area));

            if (handler is null)
            {
                _logger.Error($"Area: Unknown packet type: {packet.RawType:X4}");
                continue;
            }

            if (packet.Type == PacketType.Ping)
            {
                packet.Client.lastPing = DateTimeOffset.Now;
            }

            await handler.HandleAsync(packet.Data, packet.Client);
        }

    }
}
