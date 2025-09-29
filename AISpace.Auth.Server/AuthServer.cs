namespace AISpace.Server;

public class AuthServer : BackgroundService
{
    private readonly ILogger<AuthServer> _logger;
    private readonly MainContext _db;
    private readonly PacketDispatcher _dispatcher;
    private readonly IUserRepository _userRepo;
    private readonly IWorldRepository _worldRepo;
    private readonly ChannelReader<Packet> _channel;
    public readonly MessageDomain ActiveDomain = MessageDomain.Auth;

    private readonly TimeSpan _tickRate = TimeSpan.FromMilliseconds(1000.0 / 60.0);

    public AuthServer(ILogger<AuthServer> logger,
        MainContext db,
        IUserRepository userRepo,
        AuthChannel channel,
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
        //_db.Database.EnsureCreated();
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
            _logger.LogInformation("Dispatching {domain} packet of type {type}", ActiveDomain, packet.Type);
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
