using TrainAppMOTD.API.Models;

namespace TrainAppMOTD.API.Services.Inteface
{
    public interface IDailyMOTDService
    {
        Task<string> GetDailyMessageAsync();
        void AddDailyMessageToBase(DailyMOTD dailyMOTD);
        Task<DailyMOTD> ReadDailyMessageFromBase(DateTime dateTime);
    }
}