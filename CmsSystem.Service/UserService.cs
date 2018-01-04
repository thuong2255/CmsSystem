using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;

namespace CmsSystem.Service
{
    public interface IUserService
    {
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
    }
}