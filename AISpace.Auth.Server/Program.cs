using System.Text;
using AISpace.Area.Server;
using AISpace.Msg.Server;
using NLog;

namespace AISpace.Auth.Server;

internal class Program
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    static async Task Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        AuthServer authServer = new(50050);
        authServer.Start();
        MsgServer msgServer = new(50052);
        msgServer.Start();
        AreaServer areaServer = new(50054);
        areaServer.Start();
        await Task.Delay(-1); // Wait forever
    }
}
