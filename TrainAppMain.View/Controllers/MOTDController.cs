using Commons;
using MassTransit;
using MassTransit.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TrainAppMain.View.Controllers
{

    public class MOTDController : Controller
    {
        private readonly IRequestClient<OrderRequestSpecification> requestClient;

        public MOTDController(IRequestClient<OrderRequestSpecification> requestClient)
        {
            this.requestClient = requestClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DailyMOTDRespondDTO dailyMOTDRespondDTO)
        {

            return View("motd", dailyMOTDRespondDTO);
        }

        [HttpPost]
        public async Task<IActionResult> MotdShow()
        {
            OrderRequestSpecification orderRequestSpecification = new OrderRequestSpecification() { OrderTypeName = "motd" };

            var response = await requestClient.GetResponse<DailyMOTDRespondDTO>(orderRequestSpecification);
            return RedirectToAction("Index", response.Message);
        }
    }
}
