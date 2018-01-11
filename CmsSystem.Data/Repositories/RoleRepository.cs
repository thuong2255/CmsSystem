using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CmsSystem.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetListRoleByUserId(int userId);
    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Role> GetListRoleByUserId(int userId)
        {
            var result = from a in DbContext.RoleUsers
                         join b in DbContext.Roles on a.RoleId equals b.Id
                         where a.UserId == userId
                         select b;

            return result.ToList();
        }
    }
}