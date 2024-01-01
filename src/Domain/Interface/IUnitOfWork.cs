using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : Entity;

        Task<bool> Save();

        Task Rollback();
    }
}
