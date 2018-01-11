using AutoMapper;
using CmsSystem.Common.Constants;
using CmsSystem.Model.Models;
using CmsSystem.Service;
using CmsSystem.Web.Infrastructure.Extensions;
using CmsSystem.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web.Script.Serialization;
using CmsSystem.Web.CustomeAuthosize;

namespace CmsSystem.Web.Controllers
{
    [CustomeAuthorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IActionService _actionService;
        private readonly IFunctionService _functionService;
        private readonly IActionRoleService _acctionRoleService;
        private readonly IFunctionRoleService _functionRoleService;

        public RoleController(IRoleService roleService, IActionService actionService, IFunctionService functionService, IActionRoleService acctionRoleService, IFunctionRoleService functionRoleService)
        {
            _roleService = roleService;
            _actionService = actionService;
            _functionService = functionService;
            _acctionRoleService = acctionRoleService;
            _functionRoleService = functionRoleService;
        }

        // GET: Role
        public ActionResult Index()
        {
            var roles = _roleService.GetAll();
            var rolesVm = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleVm>>(roles);
            return View(rolesVm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleVm roleVm)
        {
            if (ModelState.IsValid)
            {
                var role = new Role();
                roleVm.CreatedBy = 1;
                roleVm.CreatedDate = DateTime.Now;
                role.UpdateRole(roleVm);
                _roleService.Add(role);
                _roleService.Save();

                TempData["MessageSuccess"] = Message.CreateSuccess;
                return RedirectToAction("Index");
            }
            TempData["MessageError"] = Message.CreateError;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var role = _roleService.GetSingleById(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            var roleVm = Mapper.Map<Role, RoleVm>(role);

            return View(roleVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleVm roleVm)
        {
            if (ModelState.IsValid)
            {
                var role = _roleService.GetSingleById(roleVm.Id);
                role.UpdateRole(roleVm);
                _roleService.Update(role);
                _roleService.Save();
                TempData["MessageSuccess"] = Message.EditSuccess;
                return RedirectToAction("Index");
            }
            TempData["MessageError"] = Message.EditError;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var role = _roleService.GetSingleById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            _roleService.Delete(role);
            _roleService.Save();
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Authorize(int id)
        {

            Session["CurrentRoleId"] = id;

            //Lấy danh sách action đã được gán cho Role
            var listActionGranted = (from a in _actionService.GetListActionByRoleId(id)
                                     select new RightAction { Id = a.Id, ParentId = a.ParentId ?? 0, Name = a.Name, IsGranted = true }).ToList();

            //Lấy tất cả danh sách action
            var listAction = (from b in _actionService.GetAll()
                              select new RightAction { Id = b.Id, ParentId = b.ParentId ?? 0, Name = b.Name, IsGranted = false }).ToList();

            //Lấy tất cả danh sách function được gán cho Role
            var listFunctionGranted = (from c in _functionService.GetListFunctionByRoleId(id)
                                       select new RightFunction { Id = c.Id, Code = c.Code, Description = c.Description, IsGranted = true }).ToList();

            //Lấy tất cả danh sách function
            var listFunction = (from d in _functionService.GetAll()
                                select new RightFunction { Id = d.Id, Code = d.Code, Description = d.Description, IsGranted = false }).ToList();

            foreach (var function in listFunction)
            {
                if (!listFunctionGranted.Select(x => x.Id).Contains(function.Id))
                {
                    listFunctionGranted.Add(function);
                }
            }


            foreach (var action in listAction)
            {
                if (!listActionGranted.Select(x => x.Id).Contains(action.Id))
                {
                    listActionGranted.Add(action);
                }
            }

            var listActionTreeView = new List<ActionTreeViewModel>();

            var menuParent = _actionService.GetMenuParent();

            foreach (var parent in menuParent)
            {
                var actionTreeView = new ActionTreeViewModel
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    Actions = listActionGranted.Where(x => x.ParentId == parent.Id).ToList()
                };
                listActionTreeView.Add(actionTreeView);
            }

            ViewBag.ListFunctionGranted = listFunctionGranted;
            return View(listActionTreeView);
        }

        [HttpGet]
        public ActionResult UpdateAuthozation(string listMenu, string listFunction)
        {

            var currentRoleId = (int)Session["CurrentRoleId"];

            var listMenuId = new JavaScriptSerializer().Deserialize<List<int>>(listMenu);

            var listFunctionId = new JavaScriptSerializer().Deserialize<List<int>>(listFunction);


            if (listMenuId.Count == 0 && _acctionRoleService.GetActionRolesByRoleId(currentRoleId).ToList().Count > 0)
            {
                _acctionRoleService.RemoveActionRoleByRoleId(currentRoleId);
                _acctionRoleService.Save();
            }

            if (listMenuId.Count > 0)
            {
                _acctionRoleService.RemoveActionRoleByRoleId(currentRoleId);

                foreach (var item in listMenuId)
                {
                    var actionRole = new ActionRole
                    {
                        RoleId = currentRoleId,
                        ActionId = item,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now
                    };
                    _acctionRoleService.AddActionToRole(actionRole);
                }
                _acctionRoleService.Save();
            }

            if (listFunctionId.Count == 0 && _functionRoleService.GetFunctionRolesByRoleId(currentRoleId).ToList().Count > 0)
            {
                _functionRoleService.RemoveFunctionRoleByRoleId(currentRoleId);
                _acctionRoleService.Save();
            }

            if (listFunctionId.Count > 0)
            {
                _functionRoleService.RemoveFunctionRoleByRoleId(currentRoleId);
                foreach (var item in listFunctionId)
                {
                    var functionRole = new FunctionRole
                    {
                        RoleId = currentRoleId,
                        FunctionId = item,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now
                    };
                    _functionRoleService.AddFunctionToRole(functionRole);
                }
                _functionRoleService.Save();
            }
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}