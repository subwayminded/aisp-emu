using System.Collections.Concurrent;
using AISpace.Common.Network;

namespace AISpace.Common.Game;

public class SharedState
{
    public ConcurrentDictionary<Guid, ClientConnection> AuthClients = new();
    public ConcurrentDictionary<Guid, ClientConnection> MsgClients = new();
    public ConcurrentDictionary<Guid, ClientConnection> AreaClients = new();
    public ConcurrentQueue<(string id, string message)> newMessages = new();

    public void RegisterClient(string serverName, ClientConnection client)
    {
        if (serverName == "Auth") AuthClients[client.Id] = client;
        else if (serverName == "Msg") MsgClients[client.Id] = client;
        else if (serverName == "Area") AreaClients[client.Id] = client;
    }

    public void UnregisterClient(string serverName, Guid clientId)
    {
        AuthClients.TryRemove(clientId, out _);
        MsgClients.TryRemove(clientId, out _);
        AreaClients.TryRemove(clientId, out _);
    }
}