using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Entities { get; }
        Task Adicionar(T entity);
        Task<T> ObterPorId(int id);
        Task<List<T>> ObterTodos();
        Task Atualizar(T entity);
        Task Remover(T entity);
        Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
