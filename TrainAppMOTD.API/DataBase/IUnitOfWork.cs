using TrainAppMOTD.API.DataBase.DAL.Interfaces;

namespace TrainAppMOTD.API.DataBase
{
    public interface IUnitOfWork
    {
        IMotdRepository motdRepository { get; }
    }
}