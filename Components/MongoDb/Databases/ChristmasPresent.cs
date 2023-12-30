using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Christmas.Components.MongoDb;

[PrimaryKey(nameof(_id))]
public class ChristmasPresent
{
    public ObjectId _id { get; set; }
    public string recipient { get; set; }
    public string gift { get; set; }
    public bool gotten { get; set; }
}
