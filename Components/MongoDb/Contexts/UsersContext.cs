using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Christmas.Components.MongoDb.Contexts;

public class UsersContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ChristmasPresent> ChristmasPresents { get; set; }
    
    public DbSet<ChristmasPasswords> ChristmasPasswords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ChristmasPresent>().ToCollection("Christmas Presents");
        modelBuilder.Entity<ChristmasPasswords>().ToCollection("Christmas Passwords");
    }
}
