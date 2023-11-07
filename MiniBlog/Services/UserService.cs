using MiniBlog.Model;
using MiniBlog.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository = null!;

        public UserService(IArticleRepository articleRepository, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers() => await userRepository.GetUsers();
        public async Task<User> GetByName(string username) => await userRepository.GetUserByName(username);
    }
}
