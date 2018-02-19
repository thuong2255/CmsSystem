using AutoMapper;
using CmsSystem.Common.Constants;
using CmsSystem.Model.Models;
using CmsSystem.Service;
using CmsSystem.Web.CustomeAuthosize;
using CmsSystem.Web.Infrastructure.Extensions;
using CmsSystem.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CmsSystem.Web.Controllers
{
    [CustomeAuthorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly IRoleService _roleService;
        private readonly IRoleUserService _roleUserService;

        public UserController(IUserService userService, ICommonService commonService, IRoleService roleService, IRoleUserService roleUserService)
        {
            _userService = userService;
            _commonService = commonService;
            _roleService = roleService;
            _roleUserService = roleUserService;
        }

        [HttpGet]
        public ActionResult Index(string search, int? page)
        {
            UserIndexViewModel viewModel = new UserIndexViewModel();

            var users = _userService.GetAllUser();
            var usersVm = Mapper.Map<IEnumerable<User>, IEnumerable<UserVm>>(users);


            if (!string.IsNullOrEmpty(search))
            {
                usersVm = usersVm.Where(x => x.FullName.ToLower().Contains(search.ToLower()) ||
                                        x.Email.ToLower().Contains(search.ToLower()) ||
                                        x.UserName.ToLower().Contains(search.ToLower()) ||
                                        x.Mobile.Contains(search));
                viewModel.Search = search;
            }

            const int PageItems = 3;
            int currentPage = page ?? 1;
 
            viewModel.UsersVm = usersVm.ToPagedList(currentPage, PageItems);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVm userVm)
        {
            if(ModelState.IsValid)
            {
                if(_userService.CheckExistingUser(userVm.UserName))
                {
                    TempData["MessageError"] = Message.ExistingUser;
                    ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name");
                    return View();
                }

                var user = new User();
                user.UpdateUser(userVm);
                _userService.Add(user);
                _userService.Save();
                TempData["MessageSuccess"] = Message.CreateSuccess;
                return RedirectToAction("Index");
            }
            TempData["MessageError"] = Message.CreateError;
            ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name");
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _userService.GetByUserId(id);

            if(user == null)
            {
                return HttpNotFound();
            }
            var userVm = Mapper.Map<User, UserVm>(user);

            ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name", userVm.Status);

            return View(userVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserVm userVm)
        {
            if(ModelState.IsValid)
            {
                var user = new User();
                user.UpdateUser(userVm);

                _userService.Update(user);
                _userService.Save();
                TempData["MessageSuccess"] = Message.EditSuccess;
                return RedirectToAction("Index");
            }
            ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name", userVm.Status);
            TempData["MessageError"] = Message.EditError;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _userService.GetByUserId(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            _userService.Delete(user.Id);
            _userService.Save();
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Authorize(int id)
        {

            Session["SelectedUserId"] = id;

            var listRoleGranted = (from a in _roleService.GetListRoleByUserId(id)
                                   select new RightRole { Id = a.Id, Description = a.Description, IsGranted = true, Name = a.Name }).ToList();

            var listAllRole = (from b in _roleService.GetAll()
                               select new RightRole { Id = b.Id, Description = b.Description, IsGranted = false, Name = b.Name }).ToList();

            foreach(var item in listAllRole)
            {
                if(!listRoleGranted.Select(x => x.Id).Contains(item.Id))
                {
                    listRoleGranted.Add(item);
                }
            }

            return View(listRoleGranted);
        }

        [HttpGet]
        public ActionResult UpdateAuthozation(string listRole)
        {
            var listRoleId = new JavaScriptSerializer().Deserialize<List<int>>(listRole);

            var userLoginId = (int)Session["UserId"];
            var SelectedUserId = (int)Session["SelectedUserId"];

            if (listRoleId.Count == 0 && _roleUserService.GetRoleUsersByUserId(SelectedUserId).ToList().Count > 0)
            {
                _roleUserService.RemoveRoleUserByUserId(SelectedUserId);
                _roleUserService.Save();
            }

            if(listRoleId.Count > 0)
            {
                _roleUserService.RemoveRoleUserByUserId(SelectedUserId);

                foreach(var item in listRoleId)
                {
                    var roleUser = new RoleUser
                    {
                        UserId = SelectedUserId,
                        RoleId = item,
                        CreatedBy = userLoginId,
                        CreatedDate = DateTime.Now
                    };
                    _roleUserService.Add(roleUser);
                }
                _roleUserService.Save();
            }

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}