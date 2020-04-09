using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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



        public IActionResult Index(string view = "")
        {
            if (string.IsNullOrEmpty(view)) {
                return View(repository.Views.Where(x => x.Location == "NotSpecified").FirstOrDefault() );
            } else
                return View(repository.Views.Where(x => x.Location == view).FirstOrDefault());
            
        }

        [HttpPost]
        public ViewResult SaveItem() {
            return View("Editor");
        }
    }
}