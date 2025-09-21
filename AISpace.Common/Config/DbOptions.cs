namespace AISpace.Common.Config;

public sealed class DbOptions
{
    public string Provider { get; set; } = "sqlite";
    public string ConnectionString { get; set; } = "Data Source=./game.db";
}
