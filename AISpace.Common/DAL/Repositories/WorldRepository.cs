using AISpace.Common.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.DAL.Repositories;

public class WorldRepository : IWorldRepository
{
    private readonly MainContext _context;

    public WorldRepository(MainContext context)
    {
        _context = context;

    }
    public async Task AddWorldAsync(string name, string description)
    {
        if ((await GetWorldByNameAsync(name)) != null)
            return;
        var world = new World { Name = name, Description = description };
        _context.Worlds.Add(world);
        await _context.SaveChangesAsync();
    }

    public async Task<List<World>> GetAllWorldsAsync()
    {
        return await _context.Worlds.ToListAsync();
    }

    public async Task<World?> GetWorldByIDAsync(int id)
    {
        return await _context.Worlds.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<World?> GetWorldByNameAsync(string name)
    {
        return await _context.Worlds.FirstOrDefaultAsync(w => w.Name == name);
    }
}
