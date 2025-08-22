using MongoDB.Driver;

public class LevelService
{
    private readonly IMongoCollection<Level> _levels;

    public LevelService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("LevelDb"));
        var database = client.GetDatabase("LevelDb");
        _levels = database.GetCollection<Level>("Levels");
    }

    // Paginated + searchable query
    public (List<Level>, long totalCount) Get(int page, int pageSize, string? search)
    {
        var filter = string.IsNullOrWhiteSpace(search)
            ? Builders<Level>.Filter.Empty
            : Builders<Level>.Filter.Regex(l => l.Name, new MongoDB.Bson.BsonRegularExpression(search, "i"));

        var totalCount = _levels.CountDocuments(filter);

        var levels = _levels.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToList();

        return (levels, totalCount);
    }

    public Level? Get(string id)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(id, out var objectId))
            throw new ArgumentException("Invalid id format");

        return _levels.Find(level => level.Id == id).FirstOrDefault();
    }

    public Level Create(Level level)
    {
        _levels.InsertOne(level);
        return level;
    }

    public void Remove(string id)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(id, out var objectId))
            throw new ArgumentException("Invalid id format");

        _levels.DeleteOne(level => level.Id == id);
    }
}
