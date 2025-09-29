namespace AISpace.Common.Config;

public class ServerOptions
{
    NetworkOptions NetworkOptions { get; set; }
    DbOptions DbOptions { get; set; }
    bool AuthServerEnabled { get; set; } = true;
    bool MsgServerEnabled { get; set; } = true;
    bool AreaServerEnabled { get; set; } = true;
}
