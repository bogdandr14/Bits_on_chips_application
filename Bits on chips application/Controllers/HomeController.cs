using Bits_on_chips_application.Models;
using Bits_on_chips_application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bits_on_chips_application.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ComponentService _componentService;
        public HomeController(ILogger<HomeController> logger, ComponentService componentService)
        {
            _logger = logger;
            _componentService = componentService;
        }

        [Microsoft.AspNetCore.Mvc.Route("")]
        [Microsoft.AspNetCore.Mvc.Route("Home")]
        [Microsoft.AspNetCore.Mvc.Route("Index")]
        [Microsoft.AspNetCore.Mvc.Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.Route("Contact")]
        [Microsoft.AspNetCore.Mvc.Route("Home/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.Route("AccessDenied")]
        [Microsoft.AspNetCore.Mvc.Route("Home/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Home/Search")]
        public Microsoft.AspNetCore.Mvc.ActionResult Search(string? term)
        {
            if(term == null)
            {
                term = "";
            }
            ViewData["CurrentFilter"] = term;
            IList<Component> components = _componentService.GetComponentsByCondition(c => c.ComponentName.Contains(term));
            return View(components);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("Home/Autocomplete")]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> Autocomplete(string? term)
        {
            if (term == null)
            {
                term = "";
            }
            var names = _componentService.SearchComponentNames(term);
            return Ok(names);
        }          


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
