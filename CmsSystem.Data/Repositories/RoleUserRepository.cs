using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;

namespace CmsSystem.Data.Repositories
{
    public interface IRoleUserRepository : IRepository<RoleUser>
    {
    }

    public class RoleUserRepository : Repository<RoleUser>, IRoleUserRepository
    {
        public RoleUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}