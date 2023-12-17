using Christmas.Components.MongoDb.Contexts;
using Christmas.Components.MongoDbCollections;

namespace Christmas.Components.Services;

public class ChristmasPresentService(UsersContext usersContext)
{
    public IEnumerable<ChristmasPresent> GetPresentsForNotThisUser(string username) =>
        usersContext.ChristmasPresents.Where(present => present.recipient != username).ToList();
}
