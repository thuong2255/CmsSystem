using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;
using System.Collections.Generic;
using System;

namespace CmsSystem.Service
{
    public interface IFunctionService
    {
        IEnumerable<Function> GetAll();

        Function GetSingleById(int id);

        void Add(Function entity);

        void Update(Function entity);

        void Delete(Function entity);

        IEnumerable<Function> GetListFunctionByRoleId(int id);


        void Save();
    }

    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FunctionService(IFunctionRepository functionRepository, IUnitOfWork unitOfWork)
        {
            _functionRepository = functionRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(Function entity)
        {
            _functionRepository.Add(entity);
        }

        public void Delete(Function entity)
        {
            _functionRepository.Delete(entity.Id);
        }

        public IEnumerable<Function> GetAll()
        {
            return _functionRepository.GetAll();
        }

        public IEnumerable<Function> GetListFunctionByRoleId(int id)
        {
            return _functionRepository.GetListFunctionByRoleId(id);
        }

        public Function GetSingleById(int id)
        {
            return _functionRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Function entity)
        {
            _functionRepository.Update(entity);
        }
    }
}