using Microsoft.AspNetCore.Mvc;

namespace TrainApp.VIEW.Controllers
{
    public class MOTDController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("test");
        }
    }
}
