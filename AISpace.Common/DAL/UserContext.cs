using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.DAL;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=users.db");
}
