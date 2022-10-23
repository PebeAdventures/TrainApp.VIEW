//using Microsoft.AspNetCore.Mvc;
//using TrainAppMOTD.API.Services;

//namespace TrainAppMOTD.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class HomeController : Controller
//    {
//        private readonly HttpClient _client;

//        public HomeController(HttpClient client)
//        {
//            _client = client;
//        }

//        [HttpPost]
//        public async Task<IActionResult> IndexAsync()
//        {
//            var respondMessage = await new DailyMOTDService(_client).GetDailyMessageAsync();

//            return Ok(respondMessage);
//        }
//    }
//}

