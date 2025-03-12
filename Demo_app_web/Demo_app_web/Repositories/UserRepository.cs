using Demo_app_web.Data;
using Demo_app_web.Models;

namespace Demo_app_web.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SE06304DemoContext demoProtectContext) : base(demoProtectContext)
        {
        }
        public User GetUserByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username);
        }
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
