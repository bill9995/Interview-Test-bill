using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IUserRepository
{
    IEnumerable<dynamic> GetUsers();
    dynamic GetUserById(string id);
    int CreateUser();
}