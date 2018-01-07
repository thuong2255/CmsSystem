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

            const int PageItems = 10;
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
                TempData["MessageError"] = Message.CreateSuccess;
            }
            return View();
        }

    }
}