using System.Collections.Concurrent;

namespace AISpace.Common.Game;

public class SharedState
{
    public ConcurrentQueue<(string id, string message)> newMessages = new();

    public ConcurrentQueue<(string id, MovementData moveData)> newMovement = new();
}
