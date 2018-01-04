using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CmsSystem.Data.Repositories
{
    public interface IFunctionRepository : IRepository<Function>
    {
        IEnumerable<Function> GetListFunctionByRoleId(int id);
    }

    public class FunctionRepository : Repository<Function>, IFunctionRepository
    {
        public FunctionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Function> GetListFunctionByRoleId(int id)
        {
            var result = from a in DbContext.FunctionRoles
                         join b in DbContext.Functions on a.FunctionId equals b.Id
                         where a.RoleId == id
                         select b;
            return result.ToList();
        }
    }
}