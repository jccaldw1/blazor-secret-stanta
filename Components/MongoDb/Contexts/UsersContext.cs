using Christmas.Components.MongoDbCollections;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Christmas.Components.MongoDb.Contexts;

public class UsersContext : DbContext
{
    public DbSet<ChristmasPresent> ChristmasPresents { get; set; }

    public static UsersContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<UsersContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public UsersContext(DbContextOptions options)
    : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ChristmasPresent>().ToCollection("Christmas Presents");
    }
}
