using AISpace.Common.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.DAL;

public class MainContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ServerInformation> Servers { get; set; }

    public DbSet<World> Worlds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=main.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equip>()
                    .HasOne(e => e.Chara)
                    .WithMany(c => c.Equips)
                    .HasForeignKey(e => e.ChararId);
    }
}