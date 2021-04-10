using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Bits_on_chips_application.Controllers
{
    public class StoreController : Controller
    {
        private readonly BitsOnChipsDbContext _db;
        public StoreController(BitsOnChipsDbContext db)
        {
            _db = db;
        }
        public IActionResult Categories()
        {
            IEnumerable<Category> objList = _db.DBCategories;
            return View(objList);
        }
        public IActionResult Component(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(id);
            }
            Category category = _db.DBCategories.Find(id);
            return RedirectToAction(category.CategoryName);
        }
        public IActionResult Ssd()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("ssd"));
            return View(objList);
        }
        public IActionResult Hdd()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("hdd"));
            return View(objList);
        }
        public IActionResult Ram()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("ram"));
            return View(objList);
        }
        public IActionResult Cpu()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("cpu"));
            return View(objList);
        }
        public IActionResult Gpu()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("gpu"));
            return View(objList);
        }
        public IActionResult Source()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("source"));
            return View(objList);
        }
        public IActionResult Cooler()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("cooler"));
            return View(objList);
        }
        public IActionResult Motherboard()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("motherboard"));
            return View(objList);
        }
        public IActionResult Case()
        {
            IQueryable<Component> objList = _db.DBComponents.Where(o => o.Category.CategoryName.Equals("case"));
            return View(objList);
        }
        //Get-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.DBComponents.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Post-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int ComponentId)
        {
            var obj = _db.DBComponents.Find(ComponentId);
            if(obj == null)
            {
                return NotFound();
            }
            _db.DBComponents.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Get-Create
        public IActionResult Create()
        {
            /*IEnumerable<SelectListItem> TypeDropDown = _db.DBCategories.Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;*/
            ComponentVM ComponentVM = new ComponentVM()
            {
                Component = new Component(),
                TypeDropDown = _db.DBCategories.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                })
            };
            return View(ComponentVM);
        }
        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ComponentVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.DBComponents.Add(obj.Component);
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.DBCategories.Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }
        //Get-Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.DBComponents.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            var categ = _db.DBCategories.Find(obj.CategoryId);
            if (categ == null)
            {
                return NotFound();
            }
            obj.Category = categ;
            return View(obj);
        }
        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Component component)
        {
            if (ModelState.IsValid)
            {
                _db.DBComponents.Update(component);
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return RedirectToAction("Edit", new { id = component.ComponentId });
        }
    }
}
