using Commons;
using MassTransit;
using TrainAppMOTD.API.Models;
using TrainAppMOTD.API.Services.Inteface;

namespace TrainAppMOTD.API.RabbitMQ
{
    public class OrderRequestConsumer : IConsumer<OrderRequestSpecification>
    {


        private readonly IDailyMOTDService dailyMOTD;

        public OrderRequestConsumer(IDailyMOTDService dailyMOTD)
        {
            this.dailyMOTD = dailyMOTD;
        }

        public async Task Consume(ConsumeContext<OrderRequestSpecification> context)
        {
            switch (context.Message.OrderTypeName)
            {
                case "motd":
                    await RespondMOTDMessage(context);
                    break;
                default:
                    throw new InvalidOperationException("Order not found");

                    break;
            }
        }

        private async Task RespondMOTDMessage(ConsumeContext<OrderRequestSpecification> context)
        {

            DailyMOTDRespondDTO dailyMOTDRespondDTO = new DailyMOTDRespondDTO();
            DateTime dateTime = DateTime.Now;
            var motdMessage = await dailyMOTD.ReadDailyMessageFromBase(dateTime);
            if (motdMessage != null)
            {
                await SendRespond(context, dailyMOTDRespondDTO, motdMessage);
            }
            else
            {
                string newMotdMessage = await dailyMOTD.GetDailyMessageAsync();
                DailyMOTD newDailyMOTD = new DailyMOTD() { Motd = newMotdMessage, Date = dateTime };
                await SendRespond(context, dailyMOTDRespondDTO, newDailyMOTD);
                dailyMOTD.AddDailyMessageToBase(newDailyMOTD);
            }




        }

        private async Task SendRespond(ConsumeContext<OrderRequestSpecification> context, DailyMOTDRespondDTO dailyMOTDRespondDTO, DailyMOTD motdMessage)
        {
            List<DailyMOTDRespondDTO> results2 = (List<DailyMOTDRespondDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject<List<DailyMOTDRespondDTO>>(motdMessage.Motd);

            dailyMOTDRespondDTO.a = results2.Select(x => x.a).FirstOrDefault();
            dailyMOTDRespondDTO.h = results2.Select(x => x.h).FirstOrDefault();
            dailyMOTDRespondDTO.q = results2.Select(x => x.q).FirstOrDefault();


            await context.RespondAsync<DailyMOTDRespondDTO>(new DailyMOTDRespondDTO { a = dailyMOTDRespondDTO.a, h = dailyMOTDRespondDTO.h, q = dailyMOTDRespondDTO.q });
        }
    }
}
