using System;
using System.Collections.Generic;
using System.Linq;
using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;

namespace CmsSystem.Service
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();

        Role GetSingleById(int id);

        void Add(Role role);

        void Update(Role role);

        void Delete(Role role);

        void Save();
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(Role role)
        {
            _roleRepository.Add(role);
        }

        public void Delete(Role role)
        {
            _roleRepository.Delete(role.Id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetSingleById(int id)
        {
            return _roleRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }
    }
}
