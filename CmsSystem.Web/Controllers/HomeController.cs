using System.Collections.Generic;
using CmsSystem.Service;
using System.Web.Mvc;
using CmsSystem.Model.Models;
using AutoMapper;
using CmsSystem.Web.Models;
using CmsSystem.Web.CustomeAuthosize;
using System.Linq;

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
            var userId = (int)Session["UserId"];


            var user = _userService.GetByUserId(userId);

            IEnumerable<Action> parentMenu = Enumerable.Empty<Action>();

            if (user.IsAdmin)
            {
                parentMenu = _actionService.GetMenuParent();
            }
            else
            {
                parentMenu = _actionService.GetMenuParentByUserId(userId);
            }


            return PartialView(parentMenu);
        }


        public IEnumerable<Action> GetSubMenu(int parentId)
        {
            var userId = (int)Session["UserId"];
            var user = _userService.GetByUserId(userId);

            if (user.IsAdmin)
                return _actionService.GetSubMenu(parentId);

            return _actionService.GetSubMenuByUserId(userId);
        }
    }
}