using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ContentController : Controller
    {

        private IContentRepository contentRepository;

        public ContentController(IContentRepository contentRepository)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TreeData(string dir = "")
        {

            return Json("OK");
        }
    }
}