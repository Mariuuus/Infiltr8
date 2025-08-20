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

    public List<Level> Get() => _levels.Find(level => true).ToList();
    public Level Get(string id)
    {
        Console.WriteLine($"Received id: '{id}'");
        if (!MongoDB.Bson.ObjectId.TryParse(id, out var objectId))
            throw new ArgumentException("Invalid id format");
        return _levels.Find<Level>(level => level.Id == id).FirstOrDefault();
    }
    public Level Create(Level level)
    {
        _levels.InsertOne(level);
        return level;
    }
    public Level Create(string content)
    {
        var level = new Level { Content = content };
        _levels.InsertOne(level);
        return level;
    }
    // public void Update(string id, Level levelIn) => _levels.ReplaceOne(level => level.Id == new MongoDB.Bson.ObjectId(id), levelIn);
    public void Remove(string id)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(id, out var objectId))
            throw new ArgumentException("Invalid id format");
        _levels.DeleteOne(level => level.Id == id);
    }
}
