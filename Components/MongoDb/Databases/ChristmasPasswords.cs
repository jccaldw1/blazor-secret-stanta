using MongoDB.Bson;

namespace Christmas.Components.MongoDb;

public class ChristmasPasswords
{
    public ObjectId _id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
}