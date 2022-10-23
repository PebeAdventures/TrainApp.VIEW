using TrainAppMOTD.API.Models;

namespace TrainAppMOTD.API.DataBase.DAL.Interfaces
{
    public interface IMotdRepository
    {
        Task<DailyMOTD> GetDailyMOTD(DateTime currentDay);
        void AddNewDailyMOTD(DailyMOTD dailyMOTD);
    }
}