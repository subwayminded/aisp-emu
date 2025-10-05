using AISpace.Common.DAL.Entities;

namespace AISpace.Common.DAL.Repositories;

public interface IWorldRepository
{
    Task AddWorldAsync(string name, string description, string address, ushort port);
    Task<World?> GetWorldByIDAsync(int id);

    Task<World?> GetWorldByNameAsync(string name);
    Task<List<World>> GetAllWorldsAsync();
}
