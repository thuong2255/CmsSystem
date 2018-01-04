using System;
using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;

namespace CmsSystem.Service
{
    public interface IActionRoleService
    {
        void AddActionToRole(ActionRole actionRole);

        void RemoveActionRoleByRoleId(int roleId);

        void Save();
    }

    public class ActionRoleService : IActionRoleService
    {
        private readonly IActionRoleRepository _actionRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActionRoleService(IActionRoleRepository actionRoleRepository, IUnitOfWork unitOfWork)
        {
            _actionRoleRepository = actionRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddActionToRole(ActionRole actionRole)
        {
            _actionRoleRepository.Add(actionRole);
        }

        public void RemoveActionRoleByRoleId(int roleId)
        {
            var actionRoles = _actionRoleRepository.GetMulti(x => x.RoleId == roleId);

            if(actionRoles != null)
            {
                foreach(var item in actionRoles)
                {
                    _actionRoleRepository.Delete(item);
                }
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
