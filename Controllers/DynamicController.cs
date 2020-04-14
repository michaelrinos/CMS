using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class DynamicController : Controller
    {
        #region Fields

        private IViewRepository repository;

        #endregion // Fields

        #region Constructor

        public DynamicController(IViewRepository repo) {
            repository = repo;
        }

        #endregion // Constructor



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Editor() {
            return View(new RazerView());
        }

        public IActionResult Search(string searchItems) {

            return View();
        }

        [HttpPost]
        public ViewResult SaveView(RazerView view) {
            if (ModelState.IsValid) {
                repository.SaveView(view);
                return View("Thanks");
            }
            return View();
        }
    }
}