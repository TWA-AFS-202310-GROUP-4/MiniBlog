using MiniBlog.Model;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("MiniBlog");

            userCollection = mongoDatabase.GetCollection<User>(User.CollectionName);
        }

        public async Task<User> CreateUser(User user)
        {
            await userCollection.InsertOneAsync(user);
            return await userCollection.Find(userSearch => userSearch.Name == user.Name).FirstAsync();
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await userCollection.Find(userSearch => userSearch.Name == userName).FirstAsync();
        }
    }
}