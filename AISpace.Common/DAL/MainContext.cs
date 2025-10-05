using AISpace.Common.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AISpace.Common.DAL;

public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<GameChannel> Channels { get; set; }
    //public DbSet<ServerInformation> Servers { get; set; }
    public DbSet<World> Worlds { get; set; }
    public DbSet<Character> Characters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=main.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipment>()
                    .HasOne(e => e.Chara)
                    .WithMany(c => c.Equips)
                    .HasForeignKey(e => e.CharacterId);

        //modelBuilder.Entity<UserSession>(b =>
        //{
        //    b.HasKey(x => x.Id);
        //    b.HasIndex(x => x.UserID);
        //    b.HasOne<User>().WithMany().HasForeignKey(x => x.UserID);
        //});
    }
}