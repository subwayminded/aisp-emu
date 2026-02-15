using AISpace.Common.DAL.Entities;
using AISpace.Common.Network.Handlers;
using AISpace.Common.Network.Packets;

namespace AISpace.Server;

public class AreaServer : BackgroundService
{
    private readonly ILogger<AreaServer> _logger;
    private readonly MainContext _db;
    private readonly PacketDispatcher _dispatcher;
    private readonly IUserRepository _userRepo;
    private readonly IWorldRepository _worldRepo;
    private readonly ChannelReader<Packet> _channel;
    public readonly MessageDomain ActiveDomain = MessageDomain.Area;

    private readonly TimeSpan _tickRate = TimeSpan.FromMilliseconds(1000.0 / 60.0);

    public AreaServer(ILogger<AreaServer> logger, MainContext db, IUserRepository userRepo, AreaChannel channel, IWorldRepository worldRepo, PacketDispatcher dispatcher)
    {
        _logger = logger;
        _db = db;
        _channel = channel.Channel;
        _dispatcher = dispatcher;
        _userRepo = userRepo;
        _worldRepo = worldRepo;

        //Setup DB
        _db.Database.EnsureCreated();

        if (!db.Items.Any())
        {
            List<Item> items = [];
            _logger.LogInformation("Loading items from CSV");
            foreach (var row in File.ReadLines("testitems.csv"))
                items.Add(new Item { Id = int.Parse(row.Split(',')[0]), Name = row.Split(',')[2] });

            //Deduplicate items by Id
            items = [.. items.DistinctBy(i => i.Id)];

            db.ChangeTracker.AutoDetectChangesEnabled = false;
            db.Items.AddRange(items);
            db.SaveChanges();
            db.ChangeTracker.AutoDetectChangesEnabled = true;
            _logger.LogInformation("Loaded {count} items", items.Count);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Starting {domain} server", ActiveDomain);
        var packetLoop = RunPacketLoop(ct);
        var gameLoop = RunGameLoop(ct);

        await Task.WhenAll(packetLoop, gameLoop);
    }

    private async Task RunPacketLoop(CancellationToken ct)
    {
        await foreach (var packet in _channel.ReadAllAsync(ct))
        {
            await _dispatcher.DispatchAsync(ActiveDomain, packet.Type, packet.Data, packet.Client, ct);
        }
    }

    private async Task RunGameLoop(CancellationToken ct)
    {
        var sw = new PeriodicTimer(_tickRate);
        while (await sw.WaitForNextTickAsync(ct))
        {
            // Advance game simulation
            UpdateWorld();
        }
    }

    private void UpdateWorld()
    {
        // game state update logic goes here
    }
}
