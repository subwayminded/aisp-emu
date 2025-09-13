using AISpace.Common.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MainContext _context;

    public UserRepository(MainContext context)
    {
        _context = context;

    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username && u.Password == password);
    }

    public async Task AddUserAsync(string username, string password)
    {
        if ((await GetUserByUsernameAsync(username)) != null)
            return;

        var user = new User { Username = username, Password = password };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task AddSessionAsync(uint userId, string otp)
    {
        var session = new UserSession { UserID = userId, OTP = otp };
        _context.UserSessions.Add(session);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ValidateSessionAsync(int userId, string otp)
    {
        return await _context.UserSessions
            .AnyAsync(s => s.Id == userId && s.OTP == otp);
    }

    public async Task<UserSession?> GetSessionAsync(int userId, string otp)
    {
        return await _context.UserSessions.FirstOrDefaultAsync(s => s.Id == userId && s.OTP == otp);
    }
}
