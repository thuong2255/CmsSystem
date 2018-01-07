using AutoMapper;
using CmsSystem.Common.Constants;
using CmsSystem.Service;
using CmsSystem.Web.Infrastructure.Extensions;
using CmsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CmsSystem.Web.Controllers
{
    public class ActionController : Controller
    {
        private readonly IActionService _actionService;

        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }

        // GET: Action
        public ActionResult Index()
        {
            var actions = _actionService.GetAll();
            var actionsVm = Mapper.Map<IEnumerable<Model.Models.Action>, IEnumerable<ActionVm>>(actions);
            return View(actionsVm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(_actionService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActionVm actionVm)
        {
            if (ModelState.IsValid)
            {
                var newAction = new Model.Models.Action();
                newAction.UpdateAction(actionVm);
                newAction.CreatedDate = DateTime.Now;
                newAction.CreatedBy = 1;
                _actionService.Add(newAction);
                _actionService.Save();
                TempData["MessageSuccess"] = Message.CreateSuccess;
                return RedirectToAction("Index");
            }
            TempData["MessageError"] = Message.CreateError;
            ViewBag.ParentId = new SelectList(_actionService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var action = _actionService.GetSingleById(id);

            var actionVm = Mapper.Map<Model.Models.Action, ActionVm>(action);

            if (action == null)
            {
                return HttpNotFound();
            }

            ViewBag.ParentId = new SelectList(_actionService.GetAll(), "Id", "Name", actionVm.ParentId);
            return View(actionVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActionVm action)
        {
            if (ModelState.IsValid)
            {
                TempData["MessageSuccess"] = Message.EditSuccess;
                return RedirectToAction("Index");
            }
            TempData["MessageError"] = Message.EditError;
            ViewBag.ParentId = new SelectList(_actionService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var package = _actionService.GetSingleById(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            _actionService.Delete(package);
            _actionService.Save();
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}