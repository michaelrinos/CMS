using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class DynamicController : Controller
    {
        #region Constants

        private const string cshtml = ".cshtml";
        private const string editor = "Editor ";

        #endregion // Constants

        #region Fields

        private IViewRepository repository;
        private readonly IConfiguration _Config;

        #endregion // Fields

        #region Constructor

        public DynamicController(IViewRepository repo, IConfiguration config ) {
            repository = repo;
            _Config = config;
        }

        #endregion // Constructor

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DisplayView(string view) {
            return View(view, new RazerView());
        }

        public IActionResult Editor(string mode, string location = "") {
            //return View(new RazerView());
            

            if (location == string.Empty && mode == string.Empty) { // We are creating a new model and we don't care about the view mode
                return View(new RazerView());
            } else {
                if (location == string.Empty) {   // We want a specific mode
                    return View(mode);
                } else {                          // Specific editor and existing model
                    try {
                        var provider = new ExampleDataProvider(_Config.GetConnectionString("CMSConnection"));
                        var view = provider.GetRazerView(location) ?? provider.GetRazerViewLikeLocation(location);
                        return View(mode + cshtml, view ?? new RazerView());
                    } catch (Exception) {
                        return View(mode + cshtml, new RazerView());
                    }
                }
            }
        }

        public IActionResult Search(string searchItems) {

            return View();
        }

        [HttpPost]
        public ViewResult SaveView(RazerView view) {
            if (ModelState.IsValid) {
                new ExampleDataProvider(_Config.GetConnectionString("CMSConnection")).CreateRazerView(view);
                return View("Thanks");
            }
            return View();
        }
    }
}