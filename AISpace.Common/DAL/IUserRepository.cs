namespace AISpace.Common.DAL;

public interface IUserRepository
{
    Task<bool> ValidateCredentialsAsync(string username, string password);
    Task AddUserAsync(string username, string password);
    Task<User?> GetUserAsync(string username);
}
