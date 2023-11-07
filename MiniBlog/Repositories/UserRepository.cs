using MiniBlog.Model;
using MiniBlog.Repositories.Interface;
using MiniBlog.Stores;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;
        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("MiniBlog");
            userCollection = mongoDatabase.GetCollection<User>(User.CollectionName);
        }

        public MongoClient MongoClient { get; }

        public async Task<User> Add(User user)
        {
            await userCollection.InsertOneAsync(user);
            return await userCollection.Find(a => a.Name == user.Name).FirstAsync();
        }

        public async Task<User?> GetByName(string userName)
        {
            return await userCollection.Find(user => user.Name == userName).FirstOrDefaultAsync();
        }
    }
}
