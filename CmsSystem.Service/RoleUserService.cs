using System;
using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;
using System.Collections;
using System.Collections.Generic;

namespace CmsSystem.Service
{
    public interface IRoleUserService
    {
        void Add(RoleUser entity);

        IEnumerable<RoleUser> GetRoleUsersByUserId(int userId);

        void RemoveRoleUserByUserId(int userId);

        void Save();
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

        public void Add(RoleUser entity)
        {
            _roleUserRepository.Add(entity);
        }

        public IEnumerable<RoleUser> GetRoleUsersByUserId(int userId)
        {
            return _roleUserRepository.GetMulti(x => x.UserId == userId);
        }

        public void RemoveRoleUserByUserId(int userId)
        {
            var roleUsers = _roleUserRepository.GetMulti(x => x.UserId == userId);

            if(roleUsers != null)
            {
                foreach (var item in roleUsers)
                {
                    _roleUserRepository.Delete(item);
                }
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}