using AISpace.Common.Config;
using AISpace.Common.DAL;
using AISpace.Common.DAL.Repositories;
using AISpace.Common.Network;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AISpace.Area.Server;

internal class Program
{

    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Logging.AddConsole();

        builder.Services.Configure<ServerOptions>(builder.Configuration.GetSection("Server"));
        //Database
        builder.Services.AddDbContext<MainContext>();

        //Repo
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        //builder.Services.AddScoped<ICharacterRepository, ICharacterRepository>();
        builder.Services.AddScoped<IWorldRepository, WorldRepository>();
        var host = builder.Build();
        await host.RunAsync();


    }
}
