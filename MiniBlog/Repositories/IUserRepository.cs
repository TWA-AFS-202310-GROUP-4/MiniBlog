using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories;

public interface IUserRepository
{
    public Task<User> FindUserByName(string name);
    public Task<User> AddUser(User user);
}
