using AISpace.Common.DAL;
using AISpace.Common.Game;
using AISpace.Common.Network;
using AISpace.Common.Network.Packets;
using NLog;

namespace AISpace.Auth.Server;

internal class AuthServer
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly TcpServer server;
    private readonly UserRepository repo;
    private readonly UserContext context;

    public AuthServer(int port)
    {
        server = new TcpServer("0.0.0.0", port);
        context = new UserContext();
        context.Database.EnsureCreated();
        repo = new UserRepository(context);
    }
    public async void Start()
    {
        _logger.Info("Starting Auth server");

        _logger.Info("Starting Database connection");
        await repo.AddUserAsync("hideki@animetoshokan.org", "password");

        _logger.Info("Starting TCP Server");
        server.Start();

        _logger.Info("Starting Main Loop");
        await foreach (var packet in server.PacketReader.ReadAllAsync())
        {
            ClientContext Client = packet.Client;
            string ClientID = packet.Client.Id.ToString();
            byte[] response = new byte[5];
            var payload = packet.Data;
            switch ((PacketType)packet.Type)
            {
                case PacketType.VersionCheckRequest:
                    {
                        var req = VersionCheckRequest.FromBytes(payload);
                        _logger.Info($"Client: {ClientID} Version: {packet.Client.Version}");
                        var resp = new VersionCheckResponse(req.Major, req.Minor, req.Version);
                        response = resp.ToBytes();
                        break;
                    }
                case PacketType.AuthenticateRequest:
                    {
                        var req = AuthenticateRequest.FromBytes(payload);
                        _logger.Info($"Username: '{req.Username}', Password: {req.Password}");
                        //bool valid = await repo.ValidateCredentialsAsync(req.Username, req.Password);
                        uint userID = 31874;
                        var resp = new AuthenticateResponse(userID);
                        response = resp.ToBytes();
                        
                        break;
                    }
                case PacketType.WorldListRequest:
                    {
                        var WorldList = new List<WorldEntry>
                        {
                            new(0, "test", "test2"),
                        };
                        var worldListResponse = new WorldListResponse(WorldList);
                        response = worldListResponse.ToBytes();
                        break;
                    }
                case PacketType.WorldSelectRequest:
                    {
                        var WorldSelectReq = WorldSelectRequest.FromBytes(payload);
                        var selectedWorldID = WorldSelectReq.WorldID;
                        _logger.Info($"World Selected: {selectedWorldID}");
                        var WorldSelectResp = new WorldSelectResponse("127.0.0.1", 50052);
                        response = WorldSelectResp.ToBytes();
                        _logger.Info($"Sending Server details for {Client.Id}");
                        break;
                    }
                case PacketType.PingRequest:
                    {
                        var ping = PingRequest.FromBytes(payload);
                        response = ping.ToBytes();
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
