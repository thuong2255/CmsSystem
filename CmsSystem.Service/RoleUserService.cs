using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;

namespace CmsSystem.Service
{
    public interface IRoleUserService
    {
    }

    public class RoleUserService : IRoleUserService
    {
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleUserService(IRoleUserRepository roleUserRepository, IUnitOfWork unitOfWork)
        {
            _roleUserRepository = roleUserRepository;
            _unitOfWork = unitOfWork;
        }
    }
}