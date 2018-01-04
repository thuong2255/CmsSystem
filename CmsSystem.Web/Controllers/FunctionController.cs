using AutoMapper;
using CmsSystem.Common.Constants;
using CmsSystem.Model.Models;
using CmsSystem.Service;
using CmsSystem.Web.Infrastructure.Extensions;
using CmsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CmsSystem.Web.Controllers
{
    public class FunctionController : Controller
    {
        private readonly IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        // GET: Function
        public ActionResult Index()
        {
            var functions = _functionService.GetAll();
            var functionsVm = Mapper.Map<IEnumerable<Function>, IEnumerable<FunctionVm>>(functions);
            return View(functionsVm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FunctionVm functionVm)
        {
            if (ModelState.IsValid)
            {
                var function = new Function();
                function.UpdateFunction(functionVm);
                function.CreatedBy = 1;
                function.CreatedDate = DateTime.Now;

                _functionService.Add(function);
                _functionService.Save();

                TempData["MessageSuccess"] = Message.CreateSuccess;
                return RedirectToAction("Index");
            }
            TempData["MessageError"] = Message.CreateError;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var function = _functionService.GetSingleById(id);

            if(function == null)
            {
                return HttpNotFound();
            }

            var functionVm = Mapper.Map<Function, FunctionVm>(function);
            return View(functionVm);
        }

        [HttpPost]
        public ActionResult Edit(FunctionVm functionVm)
        {
            if(ModelState.IsValid)
            {
                var function = new Function();
                function.UpdateFunction(functionVm);

                _functionService.Update(function);
                _functionService.Save();

                TempData["MessageSuccess"] = Message.EditSuccess;

                return RedirectToAction("Index");
            }

            TempData["MessageError"] = Message.CreateError;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var function = _functionService.GetSingleById(id);
            if (function == null)
            {
                return HttpNotFound();
            }
            _functionService.Delete(function);
            _functionService.Save();
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}