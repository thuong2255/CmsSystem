using CmsSystem.Service;
using CmsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsSystem.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                if(_userService.ValidateUser(loginVm.UserName, loginVm.Password))
                {
                    var user = _userService.GetUserByUserName(loginVm.UserName);

                    user.LastLogin = DateTime.Now;

                    _userService.Update(user);
                    _userService.Save();

                    Session["UserId"] = user.Id;
                    Session["UserName"] = user.FullName;
                    return new RedirectResult("/trang-chu");
                }
                else
                {
                    ViewBag.ErrorLogin = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View("Index");
                }
            }
            return View("Index");
        }
    }
}