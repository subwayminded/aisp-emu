using AISpace.Common.DAL.Entities;

namespace AISpace.Common.DAL.Repositories;

internal interface IWorldRepository
{
    Task AddWorldAsync(string name, string description);
    Task<World?> GetWorldByIDAsync(int id);

    Task<World?> GetWorldByNameAsync(string name);
    Task<List<World>> GetAllWorldsAsync();
}
