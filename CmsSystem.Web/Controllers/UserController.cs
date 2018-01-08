using AutoMapper;
using CmsSystem.Common.Constants;
using CmsSystem.Model.Models;
using CmsSystem.Service;
using CmsSystem.Web.Infrastructure.Extensions;
using CmsSystem.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;

        public UserController(IUserService userService, ICommonService commonService)
        {
            _userService = userService;
            _commonService = commonService;
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

            userVm.Password = null;

            ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name", userVm.Status);

            return View(userVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserVm userVm)
        {
            if(string.IsNullOrEmpty(userVm.Password))
            {
                var user = _userService.GetByUserId(userVm.Id);
                userVm.Password = user.Password;
            }

            if(ModelState.IsValid)
            {
                if (_userService.CheckExistingUser(userVm.UserName))
                {
                    TempData["MessageError"] = Message.ExistingUser;
                    ViewBag.Status = new SelectList(_commonService.GetStatusUser(), "Value", "Name", userVm.Status);
                    return View();
                }

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
    }
}