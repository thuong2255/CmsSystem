using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;

namespace CmsSystem.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}