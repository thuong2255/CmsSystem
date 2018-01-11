using System.Collections.Generic;
using CmsSystem.Service;
using System.Web.Mvc;
using CmsSystem.Model.Models;
using AutoMapper;
using CmsSystem.Web.Models;
using CmsSystem.Web.CustomeAuthosize;

namespace CmsSystem.Web.Controllers
{
    [CustomeAuthorize]
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

        // GET: Home
        public ActionResult Index()
        {
            var user = _userService.GetUsersLastLogin();
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