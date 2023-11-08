using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(string userName);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> IsUserExsit(string userName);
    }
}