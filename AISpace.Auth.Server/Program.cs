using System.Text;
using AISpace.Area.Server;
using AISpace.Common.DAL;
using AISpace.Common.Network;
using AISpace.Msg.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NLog;

namespace AISpace.Auth.Server;

internal class Program
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    static async Task Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            // Register DbContext (scoped)
            services.AddDbContext<MainContext>(options =>
            {
                options.UseSqlite("Data Source=main.db");
            });
            // Register all packet handlers (scoped)
            services.Scan(scan => scan
                .FromAssemblyOf<IPacketHandler>()   // or whichever assembly holds handlers
                .AddClasses(classes => classes.AssignableTo<IPacketHandler>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Register dispatcher itself (singleton)
            services.AddSingleton<PacketDispatcher>();
        }).Build();
        AuthServer authServer = new(50050);
        authServer.Start();
        MsgServer msgServer = new(50052);
        msgServer.Start();
        AreaServer areaServer = new( );
        areaServer.Start();
        await Task.Delay(-1); // Wait forever
    }
}
