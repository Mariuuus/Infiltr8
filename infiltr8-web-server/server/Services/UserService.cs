using MongoDB.Driver;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IConfiguration config)
    {
        // TODO: figure out if we should make a new db connection?
    }

    public User Create(User user)
    {
        _users.InsertOne(user);
        return user;
    }
} 