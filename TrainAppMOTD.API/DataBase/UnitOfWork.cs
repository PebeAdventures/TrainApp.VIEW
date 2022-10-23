using TrainAppMOTD.API.DataBase.DAL.Interfaces;

namespace TrainAppMOTD.API.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMotdRepository motdRepository { get; }

        public UnitOfWork(IMotdRepository motdRepository)
        {
            this.motdRepository = motdRepository;
        }
    }
}
