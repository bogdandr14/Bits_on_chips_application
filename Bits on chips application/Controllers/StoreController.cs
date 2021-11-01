using Authentication.Jwt;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Services;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Bits_on_chips_application.Controllers
{
    public class StoreController : Controller
    {
        private readonly ComponentService _componentService;
        private readonly CategoryService _categoryService;
        public StoreController(ComponentService componentService, CategoryService categoryService)
        {
            _componentService = componentService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("Store")]
        [Route("Store/Categories")]
        public IActionResult Categories()
        {
            IEnumerable<Category> objList = _categoryService.GetCategories();
            return View(objList);
        }
    }
}
