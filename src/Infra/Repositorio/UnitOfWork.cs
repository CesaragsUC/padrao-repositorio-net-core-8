using Domain.Entidades;
using Domain.Interfaces;
using Infra.Db;
using System.Collections;

namespace Infra.Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FuncionarioContext _dbContext;
        private Hashtable _repositories;
        private bool _disposed;
        public UnitOfWork(FuncionarioContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IRepository<T> Repository<T>() where T : Entity
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(T).Name; 
            
            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);    
            }

            return (IRepository<T>) _repositories[type];
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() > 0 ;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }
    }
}
