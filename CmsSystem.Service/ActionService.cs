using System;
using System.Collections.Generic;
using System.Linq;
using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;
using CmsSystem.Model.Models;

namespace CmsSystem.Service
{
    public interface IActionService
    {
        IEnumerable<Model.Models.Action> GetAll();
        void Add(Model.Models.Action entity);

        Model.Models.Action GetSingleById(int id);

        void Update(Model.Models.Action entity);

        void Delete(Model.Models.Action entity);

        IEnumerable<Model.Models.Action> GetMenuParent();

        List<Model.Models.Action> GetSubMenu(int parentId);

        IEnumerable<Model.Models.Action> GetListActionByRoleId(int id);

        void Save();
    }
    public class ActionService: IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActionService(IActionRepository actionRepository, IUnitOfWork unitOfWork)
        {
            _actionRepository = actionRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Model.Models.Action> GetAll()
        {
            return _actionRepository.GetAll();
        }

        public void Add(Model.Models.Action entity)
        {
            _actionRepository.Add(entity);
        }

        public Model.Models.Action GetSingleById(int id)
        {
            return _actionRepository.GetSingleById(id);
        }

        public void Update(Model.Models.Action entity)
        {
            _actionRepository.Update(entity);
        }

        public void Delete(Model.Models.Action entity)
        {
            _actionRepository.Delete(entity.Id);
        }

        public IEnumerable<Model.Models.Action> GetMenuParent()
        {
            return _actionRepository.GetMulti(x => x.Id > 0 && !x.ParentId.HasValue).OrderBy(x => x.Id);
        }

        public List<Model.Models.Action> GetSubMenu(int parentId)
        {
            return _actionRepository.GetMulti(x => x.ParentId == parentId).OrderBy(x => x.Id).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Model.Models.Action> GetListActionByRoleId(int id)
        {
            return _actionRepository.GetListActionByRoleId(id);
        }
    }
}
