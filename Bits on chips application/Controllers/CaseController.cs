using Authentication.Jwt;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bits_on_chips_application.Controllers
{
    public class CaseController : Controller
    {

        private readonly CaseService _caseService;
        private readonly CategoryService _categoryService;
        private readonly WishItemService _wishItemService;
        public CaseController(CaseService caseService, CategoryService categoryService, WishItemService wishItemService)
        {
            _caseService = caseService;
            _categoryService = categoryService;
            _wishItemService = wishItemService;
        }


        [HttpGet]
        [Route("Category/Case")]
        [Route("Store/Case")]
        public IActionResult Index()
        {
            IList<Case> objList = _caseService.GetCases();
            return View(objList);
        }

        [HttpGet]
        [Route("Case/Details")]
        public IActionResult Details(int id)
        {
            string userId = JwtMiddleware.getUserId(HttpContext);
            Case obj = _caseService.GetCaseById(id);
            TempData["wish item"] = _wishItemService.GetWishItemsByCondition(x => x.UserId == userId && x.ComponentId == obj.ComponentId).FirstOrDefault();
            return View(obj);
        }

        //Get-Delete
        [Authorize]
        [HttpGet]
        [Route("Case/Delete")]
        public IActionResult Delete(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _caseService.GetCaseById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            return RedirectToAction("Index");
        }

        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Case/DeletePost")]
        public IActionResult DeletePost(int CaseId)
        {
            var obj = _caseService.GetCaseById(CaseId);
            if (obj == null)
            {
                return NotFound();
            }
            _caseService.DeleteCase(obj);
            _caseService.Save();
            return RedirectToAction("Index");
        }

        //Get-Create
        [Authorize]
        [HttpGet]
        [Route("Case/Create")]
        public IActionResult Create()
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                Case CaseVM = new Case()
                {
                    Component = new Component()
                };
                return View(CaseVM);
            }
            return RedirectToAction("Index");
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Case/Create")]
        public IActionResult Create(Case obj)
        {
            if (ModelState.IsValid)
            {
                obj.Component.CategoryId = _categoryService.GetCategoriesByCondition(x => x.CategoryName == "case").First().CategoryId;
                _caseService.AddCase(obj);
                _caseService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        //Get-Edit
        [Authorize]
        [HttpGet]
        [Route("Case/Edit")]
        public IActionResult Edit(int? id)
        {
            if (JwtMiddleware.IsUserInRole(HttpContext, Helper.Admin))
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var obj = _caseService.GetCaseById(id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
            return RedirectToAction("Index");
        }

        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Case/Edit")]
        public IActionResult EditPost(Case item)
        {
            if (ModelState.IsValid)
            {
                _caseService.UpdateCase(item);
                _caseService.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = item.CaseId });
        }
    }
}
