using Microsoft.EntityFrameworkCore;
using TrainAppMOTD.API.DataBase.Context;
using TrainAppMOTD.API.DataBase.DAL.Interfaces;
using TrainAppMOTD.API.Models;

namespace TrainAppMOTD.API.DataBase.DAL
{
    public class MotdRepository : IMotdRepository

    {
        private readonly TrainAppMotdDbContext trainAppMotdDbContext;

        public MotdRepository(TrainAppMotdDbContext trainAppMotdDbContext)
        {
            this.trainAppMotdDbContext = trainAppMotdDbContext;
        }


        public async Task<DailyMOTD> GetDailyMOTD(DateTime currentDay)
        {
            var value = await trainAppMotdDbContext.dailyMOTDs.Where(x => x.Date.Day == currentDay.Day).FirstOrDefaultAsync();
            return value;
        }

        public void AddNewDailyMOTD(DailyMOTD dailyMOTD)
        {
            trainAppMotdDbContext.dailyMOTDs.Add(dailyMOTD);
            trainAppMotdDbContext.SaveChangesAsync();
        }

    }
}
