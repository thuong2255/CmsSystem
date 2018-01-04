using System;
using CmsSystem.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CmsSystem.Data.Repositories
{
    public interface IActionRepository : IRepository<Model.Models.Action>
    {
        IEnumerable<Model.Models.Action> GetListActionByRoleId(int id);
    }

    public class ActionRepository : Repository<Model.Models.Action>, IActionRepository
    {
        public ActionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Model.Models.Action> GetListActionByRoleId(int id)
        {
            var result = from actionRole in DbContext.ActionRoles
                         join action in DbContext.Actions on actionRole.ActionId equals action.Id
                         where actionRole.RoleId == id
                         select action;

            return result.ToList();
        }
    }
}