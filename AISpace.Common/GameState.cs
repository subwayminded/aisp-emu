namespace AISpace.Common;

public class GameState
{
    private readonly ReaderWriterLockSlim _lock = new();
    private readonly uint _worldID;
    private readonly Dictionary<Guid, string> _players = new();
    private readonly Dictionary<Guid, object> _npcs = new();

    public void AddPlayer(Guid id, string name)
    {
        _lock.EnterWriteLock();
        try { _players[id] = name; }
        finally { _lock.ExitWriteLock(); }
    }

    public void RemovePlayer(Guid id)
    {
        _lock.EnterWriteLock();
        try { _players.Remove(id); }
        finally { _lock.ExitWriteLock(); }
    }

    public IReadOnlyDictionary<Guid, string> SnapshotPlayers()
    {
        _lock.EnterReadLock();
        try { return new Dictionary<Guid, string>(_players); }
        finally { _lock.ExitReadLock(); }
    }
}
