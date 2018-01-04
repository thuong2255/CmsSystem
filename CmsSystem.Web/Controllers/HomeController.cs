using System.Collections.Generic;
using CmsSystem.Service;
using System.Web.Mvc;
using CmsSystem.Model.Models;

namespace CmsSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IActionService _actionService;

        public HomeController(IRoleService roleService, IActionService actionService)
        {
            _roleService = roleService;
            _actionService = actionService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
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