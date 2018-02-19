using System;
using CmsSystem.Data.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using CmsSystem.Model.Models;

namespace CmsSystem.Data.Repositories
{
    public interface IActionRepository : IRepository<Model.Models.Action>
    {
        IEnumerable<Model.Models.Action> GetListActionByRoleId(int id);

        IEnumerable<Model.Models.Action> GetListActionByUserId(int userId);
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

        public IEnumerable<Model.Models.Action> GetListActionByUserId(int userId)
        {
            var result = from roleUser in DbContext.RoleUsers
                         join role in DbContext.Roles on roleUser.RoleId equals role.Id
                         join actionRole in DbContext.ActionRoles on role.Id equals actionRole.RoleId
                         join action in DbContext.Actions on actionRole.ActionId equals action.Id
                         where roleUser.UserId == userId
                         select action;
            return result.ToList();
        }
    }
}