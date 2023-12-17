using MongoDB.Bson;

namespace Christmas.Components.MongoDbCollections;

public class ChristmasPresent
{
    public ObjectId _id { get; set; }
    public string recipient { get; set; }
    public string gift { get; set; }
    public bool gotten { get; set; }
}
