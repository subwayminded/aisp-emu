using System.Threading.Channels;
using AISpace.Common.DAL;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;

namespace AISpace.Area.Server;

public class AreaServer : BackgroundService
{
    private readonly ILogger<AreaServer> _logger;
    private readonly MainContext _db;
    private readonly PacketDispatcher _dispatcher;
    private readonly IUserRepository _userRepo;
    private readonly IWorldRepository _worldRepo;
    private readonly ChannelReader<Packet> _channel;
    public readonly MessageDomain ActiveDomain = MessageDomain.Area;
    public AreaServer(ILogger<AreaServer> logger,
        MainContext db,
        IUserRepository userRepo,
        AreaChannel channel,
        IWorldRepository worldRepo,
        PacketDispatcher dispatcher)
    {
        _logger = logger;
        _db = db;
        _channel = channel.Channel;
        _dispatcher = dispatcher;
        _userRepo = userRepo;
        _worldRepo = worldRepo;

        //Setup DB
        _db.Database.EnsureCreated();
    }


    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Starting {domain} server", ActiveDomain);
        await foreach (var packet in _channel.ReadAllAsync(ct))
        {
            //Dispatch packet to its handler
            _logger.LogInformation("Dispatching {domain} packet of type {type}", ActiveDomain, packet.Type);
            await _dispatcher.DispatchAsync(ActiveDomain, packet.Type, packet.Data, packet.Client, ct);
        }
    }
}
