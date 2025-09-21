
using Microsoft.Extensions.Hosting;

namespace AISpace.Area.Server;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        Console.WriteLine("Hello, World!");
    }
}
