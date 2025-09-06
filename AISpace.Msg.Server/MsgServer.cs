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
            byte[] response = new byte[5];
            var payload = packet.Data;
            switch ((PacketType)packet.Type)
            {
                case PacketType.PingRequest:
                    {
                        var ping = PingRequest.FromBytes(payload);
                        response = ping.ToBytes();
                        break;
                    }
                case PacketType.VersionCheckRequest:
                    {
                        var req = VersionCheckRequest.FromBytes(payload);
                        _logger.Info($"Client: {ClientID} Version: {packet.Client.Version}");
                        var resp = new VersionCheckResponse(req.Major, req.Minor, req.Version);

                        response = resp.ToBytes();
                        break;
                    }
                case PacketType.ItemGetBaseListRequest:
                    {
                        var itemGetBaseListResp = new ItemGetBaseListResponse();
                        response = itemGetBaseListResp.ToBytes();
                        break;
                    }
                case PacketType.LoginRequest:
                    {
                        var loginReq = LoginRequest.FromBytes(payload);
                        _logger.Info($"Client: {ClientID} LoginRequest UserID: {loginReq.UserID}");
                        var loginResp = new LoginResponse();

                        response = loginResp.ToBytes();
                        break;
                    }
                case PacketType.AvatarGetDataRequest:
                    {
                        //var avatarGetDataReq = AvatarGetDataRequest.FromBytes(payload);
                        //_logger.Info($"Client: {ClientID} AvatarGetDataRequest AvatarID: {avatarGetDataReq.AvatarID}");
                        //var avatarGetDataResp = new AvatarGetDataResponse(avatarGetDataReq.AvatarID);
                        //response = avatarGetDataResp.ToBytes();
                        response = [3, 26, 1, 0, 0, 71, 103, 0, 0, 0, 0, 116, 101, 115, 116, 0, 51, 70, 15, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 29, 154, 0, 0, 0, 0, 0, 66, 164, 155, 0, 0, 0, 0, 0, 222, 29, 154, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                        _ = Client.SendAsync(response);

                        response = [ 3, 6, 0, 0, 0, 85, 176, 0, 0, 0, 0 ];
                        break;
                    }
                default:
                    {
                        _logger.Error($"Unknown packet type: {packet.Type:X4}");
                        break;
                    }
            }
            _ = Client.SendAsync(response);



        }

    }
}
