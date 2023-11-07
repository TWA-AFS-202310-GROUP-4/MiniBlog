using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
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

        public async Task CreateUser(string userName)
        {
            var user = new User(userName);
            await userCollection.InsertOneAsync(user);
        }

        public async Task<List<User>> GetAllUsersAsync() =>
            await userCollection.Find(_ => true).ToListAsync();

        public async Task<bool> IsUserExsit(string userName)
        {
            var userList = await GetAllUsersAsync();
            return userList.Exists(user => user.Name == userName);
        }
    }
}
