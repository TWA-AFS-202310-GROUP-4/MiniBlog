using MiniBlog.Model;
using System.Threading.Tasks;

namespace MiniBlog.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<User> Add(User user);
        public Task<User?> GetByName(string userName);
    }
}
