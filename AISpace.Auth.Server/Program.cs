using System.Text;
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
        await Task.Delay(-1); // Wait forever
    }
}
