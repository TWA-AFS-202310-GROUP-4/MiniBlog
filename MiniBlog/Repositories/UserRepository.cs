using MiniBlog.Model;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MiniBlog.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> userCollection;

    public UserRepository(IMongoClient mongoClient)
    {
        var mongoDatabase = mongoClient.GetDatabase("MiniBlog");

        userCollection = mongoDatabase.GetCollection<User>(User.CollectionName);
    }

    public async Task<User> AddUser(User newUser)
    {
        await userCollection.InsertOneAsync(newUser);
        return await userCollection.Find(user => user.Name.Equals(newUser.Name)).FirstAsync();
    }

    public async Task<User> FindUserByName(string name)
    {
        return await userCollection.Find(user => user.Name.Equals(name)).FirstAsync();
    }
}
