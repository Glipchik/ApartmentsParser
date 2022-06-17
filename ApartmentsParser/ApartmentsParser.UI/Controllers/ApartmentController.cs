using Microsoft.AspNetCore.Mvc;

namespace ApartmentsParser.UI.Controllers
{
    public class ApartmentController : Controller
    {
        public IActionResult FilterList()
        {
            return View();
        }
    }
}
