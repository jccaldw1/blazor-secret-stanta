using Christmas.Components.MongoDb.Contexts;
using MongoDB.Driver;

namespace Christmas.Components.Services;

public class MongoDbUserService(UsersContext usersContext)
{
    public string GetUsername(string codename)
    {
        var codenameUser = usersContext.ChristmasPasswords.First(christmasPassword => christmasPassword.password == codename);

        return codenameUser.name;
    }
}

