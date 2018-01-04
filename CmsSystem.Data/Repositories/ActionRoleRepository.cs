using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;

namespace CmsSystem.Data.Repositories
{
    public interface IActionRoleRepository : IRepository<ActionRole>
    {
    }

    public class ActionRoleRepository : Repository<ActionRole>, IActionRoleRepository
    {
        public ActionRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}