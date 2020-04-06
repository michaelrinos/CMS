using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class DynamicController : Controller
    {
        public IActionResult Index()
        {
            return View("Editor");
        }
    }
}