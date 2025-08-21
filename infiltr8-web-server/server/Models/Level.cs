using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Level
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public required string Name { get; set; }
    public required string Author { get; set; }
    public DateTime UploadDate { get; set; }
    public required string Content { get; set; }
}
