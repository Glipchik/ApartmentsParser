using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApartmentsParser.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApartmentService _apartmentService;

        public HomeController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        public IActionResult Index()
        {
            var collectionModel = _apartmentService.GetAllAsync().Result;

            return View(collectionModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
