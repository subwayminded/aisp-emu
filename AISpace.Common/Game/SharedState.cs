using System.Collections.Concurrent;

namespace AISpace.Common.Game;

public class SharedState
{
    public ConcurrentQueue<(string id, string message)> newMessages = new ConcurrentQueue<(string id, string message)>();

    public ConcurrentQueue<(string id, MovementData moveData)> newMovement = new ConcurrentQueue<(string id, MovementData moveData)>();
}
