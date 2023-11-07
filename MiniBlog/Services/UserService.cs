using MiniBlog.Model;
using MiniBlog.Repositories.Interface;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository = null!;

        public UserService(IUserRepository userRepository) 
        {  
            this.userRepository = userRepository; 
        }

        public async Task<User?> GetByName(string name)
        {
            return await userRepository.GetByName(name);
        }
    }
}
