using AISpace.Common.Game;
using AISpace.Common.Network;
using AISpace.Common.Network.Packets;
using NLog;

namespace AISpace.Msg.Server;

public class MsgServer(int port = 50052)
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly TcpServer tcpServer = new("0.0.0.0", port);

    public async void Start()
    {
        _logger.Info("Starting Msg server");

        _logger.Info("Starting TCP Server");
        tcpServer.Start();

        _logger.Info("Starting Main Loop");
        await foreach (var packet in tcpServer.PacketReader.ReadAllAsync())
        {
            ClientContext Client = packet.Client;
            string ClientID = packet.Client.Id.ToString();
            var payload = packet.Data;
            _logger.Info($"Client: Msg {((PacketType)packet.Type)}");
            switch ((PacketType)packet.Type)
            {
                case PacketType.PingRequest:
                    var ping = PingRequest.FromBytes(payload);
                    _ = Client.SendAsync(PacketType.PingResponse, ping.ToBytes());
                    break;
                case PacketType.VersionCheckRequest:
                    var req = VersionCheckRequest.FromBytes(payload);
                    _logger.Info($"Client: {ClientID} Version: {packet.Client.Version}");
                    var resp = new VersionCheckResponse(req.Major, req.Minor, req.Version);
                    _ = Client.SendAsync(PacketType.VersionCheckResponse, resp.ToBytes());
                    break;
                case PacketType.ItemGetBaseListRequest:
                    var itemGetBaseListResp = new ItemGetBaseListResponse();
                    _ = Client.SendAsync(PacketType.ItemGetBaseListResponse, itemGetBaseListResp.ToBytes());
                    break;
                case PacketType.LoginRequest:
                    var loginReq = LoginRequest.FromBytes(payload);
                    _logger.Info($"Client: {ClientID} LoginRequest UserID: {loginReq.UserID}");
                    var loginResp = new LoginResponse();
                    _ = Client.SendAsync(PacketType.LoginResponse, loginResp.ToBytes());
                    break;
                case PacketType.AvatarGetDataRequest:
                    //var avatarGetDataReq = AvatarGetDataRequest.FromBytes(payload);
                    //_logger.Info($"Client: {ClientID} AvatarGetDataRequest AvatarID: {avatarGetDataReq.AvatarID}");
                    //var avatarGetDataResp = new AvatarGetDataResponse(avatarGetDataReq.AvatarID);
                    //response = avatarGetDataResp.ToBytes();

                    //AvatarDataResponse
                    
                    var DataResp = new AvatarDataResponse(0,"test", 1001011, 0, 0);
                    DataResp.Equips.Add(new ItemSlotInfo(10100140, 0));
                    DataResp.Equips.Add(new ItemSlotInfo(10200130, 0));
                    DataResp.Equips.Add(new ItemSlotInfo(10100190, 0));
                    for (int i = 3; i < 30; i++)
                    {
                        DataResp.Equips.Add(new ItemSlotInfo(0, 0));
                    }
                    byte[] avatarGetDataResp = [3, 26, 1, 0, 0, 71, 103, 0, 0, 0, 0, 116, 101, 115, 116, 0, 51, 70, 15, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 29, 154, 0, 0, 0, 0, 0, 66, 164, 155, 0, 0, 0, 0, 0, 222, 29, 154, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

                    _logger.Info($"New: {BitConverter.ToString(DataResp.ToBytes())}");
                    _logger.Info($"Old: {BitConverter.ToString(avatarGetDataResp)}");
                    _ = Client.SendAsync(PacketType.AvatarDataResponse, DataResp.ToBytes());


                    var avatarGetDataResp2 = new AvatarGetDataResponse();
                    _ = Client.SendAsync(PacketType.AvatarGetDataResponse, avatarGetDataResp2.ToBytes());
                        break;
                default:
                        _logger.Error($"Unknown packet type: {packet.Type:X4}");
                        break;
            }



        }

    }
}
