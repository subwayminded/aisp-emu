using AISpace.Common.DAL.Entities;

namespace AISpace.Common.DAL.Repositories;

public interface IUserRepository
{
    Task<bool> ValidateCredentialsAsync(string username, string password);
    Task AddUserAsync(string username, string password);
    Task<User?> GetUserByUsernameAsync(string username);

    Task<User?> GetUserByIdAsync(int userId);

    //User Sessions
    Task<bool> ValidateSessionAsync(int userId, string otp);

    Task<UserSession?> GetSessionAsync(int userId, string otp);
}
