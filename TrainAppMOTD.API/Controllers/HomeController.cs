using Microsoft.AspNetCore.Mvc;
using TrainAppMOTD.API.Services;

namespace TrainAppMOTD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController(HttpClient client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync()
        {


            return await new DailyMOTD(_client).GetDailyMessageAsync();
        }
    }
}

