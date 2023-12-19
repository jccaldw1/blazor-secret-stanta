using Christmas.Components.MongoDb;
using Christmas.Components.MongoDb.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Components.Services;

public class ChristmasPresentService(IDbContextFactory<UsersContext> usersContextFactory)
{
    public IEnumerable<ChristmasPresent> GetPresentsForNotThisUser(string username)
    {
        using var usersContext = usersContextFactory.CreateDbContext();

        return usersContext.ChristmasPresents.Where(present => present.recipient != username).ToList();
    }

    public IEnumerable<string> GetPresentsForThisUser(string username)
    {
        using var usersContext = usersContextFactory.CreateDbContext();

        return usersContext.ChristmasPresents.Where(present => present.recipient == username).AsEnumerable().Select(present => present.gift).ToList();
    }

    public void AddChristmasPresentForThisUser(string username, string present)
    {
        using var usersContext = usersContextFactory.CreateDbContext();

        var presentToAdd = new ChristmasPresent()
        {
            gift = present,
            recipient = username,
            gotten = false
        };

        usersContext.ChristmasPresents.Add(presentToAdd);

        usersContext.SaveChanges();
    }

    public void DeleteChristmasPresentForThisUser(string username, string presentToDelete)
    {
        using var usersContext = usersContextFactory.CreateDbContext();

        var christmasPresentToDelete = usersContext.ChristmasPresents.First(present => present.recipient == username && present.gift == presentToDelete);
        usersContext.ChristmasPresents.Remove(christmasPresentToDelete);

        usersContext.SaveChanges();
    }

    public void ChangeGottenStatusOfChristmasPresentForOtherUser(ChristmasPresent present, bool gotten)
    {
        using var usersContext = usersContextFactory.CreateDbContext();

        var presentToUpdate = usersContext.ChristmasPresents.AsEnumerable().FirstOrDefault(present);

        presentToUpdate.gotten = gotten;

        usersContext.SaveChanges();
    }
}
