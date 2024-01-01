using Domain.Entidades;
using Domain.Interfaces;
using Infra.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repositorio
{
    public  class Repository<T>: IRepository<T>  where T : Entity, new()
    {
        protected readonly FuncionarioContext _dbContext;
        public Repository(FuncionarioContext db)
        {
            _dbContext = db;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public Task Adicionar(T entity)
        {
            _dbContext.Set<T>().AddAsync(entity);
            return Task.CompletedTask;
        }

        public  Task Atualizar(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<T> ObterPorId(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _dbContext
             .Set<T>()
             .ToListAsync();
        }

        public Task Remover(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
