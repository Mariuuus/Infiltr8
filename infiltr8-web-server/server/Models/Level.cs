using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Level
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Content { get; set; }
}
