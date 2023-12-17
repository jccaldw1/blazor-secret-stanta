using Christmas.Components.MongoDb.Contexts;
using MongoDB.Driver;

namespace Christmas.Components.Services;

public class MongoDbUserService(UsersContext usersContext)
{
    public bool IsValidUser(string username) =>
        usersContext.ChristmasPresents.Any(christmasPresent => christmasPresent.recipient == username);
}

