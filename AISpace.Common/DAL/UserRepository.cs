using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.DAL;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;

    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<bool> ValidateOTPAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task AddUserAsync(string username, string password)
    {
        var user = new User { Username = username, Password = password };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
