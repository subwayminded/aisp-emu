namespace AISpace.Common.Config;

public class ServerOptions
{
    public required NetworkOptions NetworkOptions { get; set; }
    public required DbOptions DbOptions { get; set; }
    bool AuthServerEnabled { get; set; } = true;
    bool MsgServerEnabled { get; set; } = true;
    bool AreaServerEnabled { get; set; } = true;
}
