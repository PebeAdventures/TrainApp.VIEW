using Microsoft.AspNetCore.Mvc;

namespace TrainAppMain.View.Controllers
{
    public class MOTDController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("motd");
        }
    }
}
