using MongoDB.Driver;
using server.Models;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("UserDb"));
        var database = client.GetDatabase("UserDb");
        _users = database.GetCollection<User>("Users");

        var indexKeys = Builders<User>.IndexKeys.Ascending(u => u.Username);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<User>(indexKeys, indexOptions);
        _users.Indexes.CreateOne(indexModel);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task<User> Create(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }
} 