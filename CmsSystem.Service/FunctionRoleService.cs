using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;
using System.Collections.Generic;
using System;

namespace CmsSystem.Service
{
    public interface IFunctionRoleService
    {
        void AddFunctionToRole(FunctionRole functionRole);

        void RemoveFunctionRoleByRoleId(int roleId);

        IEnumerable<FunctionRole> GetFunctionRolesByRoleId(int id);

        void Save();
    }

    public class FunctionRoleService : IFunctionRoleService
    {
        private readonly IFunctionRoleRepository _functionRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FunctionRoleService(IFunctionRoleRepository functionRoleRepository, IUnitOfWork unitOfWork)
        {
            _functionRoleRepository = functionRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddFunctionToRole(FunctionRole functionRole)
        {
            _functionRoleRepository.Add(functionRole);
        }

        public IEnumerable<FunctionRole> GetFunctionRolesByRoleId(int id)
        {
            return _functionRoleRepository.GetMulti(x => x.RoleId == id);
        }

        public void RemoveFunctionRoleByRoleId(int roleId)
        {
            var functionRoles = _functionRoleRepository.GetMulti(x => x.RoleId == roleId);

            if (functionRoles != null)
            {
                foreach (var item in functionRoles)
                {
                    _functionRoleRepository.Delete(item);
                }
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}