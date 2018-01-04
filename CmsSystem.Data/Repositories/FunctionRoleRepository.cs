using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;

namespace CmsSystem.Data.Repositories
{
    public interface IFunctionRoleRepository : IRepository<FunctionRole>
    {
    }

    public class FunctionRoleRepository : Repository<FunctionRole>, IFunctionRoleRepository
    {
        public FunctionRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}