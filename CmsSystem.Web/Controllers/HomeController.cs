using System.Collections.Generic;
using CmsSystem.Service;
using System.Web.Mvc;
using CmsSystem.Model.Models;
using AutoMapper;
using CmsSystem.Web.Models;

namespace CmsSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IActionService _actionService;
        private readonly IUserService _userService;

        public HomeController(IRoleService roleService, IActionService actionService, IUserService userService)
        {
            _roleService = roleService;
            _actionService = actionService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm loginVm)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Home
        public ActionResult Index()
        {
            var user = _userService.GetAllUser();
            var userVm = Mapper.Map<IEnumerable<User>, IEnumerable<UserVm>>(user);
            return View(userVm);
        }

        public ActionResult Menu()
        {
            var parentMenu = _actionService.GetMenuParent();
            return PartialView(parentMenu);
        }


        public IEnumerable<Action> GetSubMenu(int parentId)
        {
            return _actionService.GetSubMenu(parentId);
        }
    }
}