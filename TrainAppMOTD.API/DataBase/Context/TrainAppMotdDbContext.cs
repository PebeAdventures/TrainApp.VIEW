using Microsoft.EntityFrameworkCore;
using TrainAppMOTD.API.Models;

namespace TrainAppMOTD.API.DataBase.Context
{
    public class TrainAppMotdDbContext : DbContext
    {


        public DbSet<DailyMOTD> dailyMOTDs { get; set; }
        public TrainAppMotdDbContext(DbContextOptions<TrainAppMotdDbContext> options) : base(options)
        { }
    }
}
