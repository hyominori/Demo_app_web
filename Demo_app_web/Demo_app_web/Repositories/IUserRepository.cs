using Demo_app_web.Models;

namespace Demo_app_web.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);

        User GetUserByUsernameAndPassword(string username, string password);
    }
}
