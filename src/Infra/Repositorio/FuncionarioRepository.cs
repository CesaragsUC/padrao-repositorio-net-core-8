using Domain.Entidades;
using Domain.Interfaces;
using Infra.Db;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        private readonly FuncionarioContext _db;
        public FuncionarioRepository(FuncionarioContext db):base(db)
        {
            _db = db;
        }

        public async Task<List<Funcionario>> ObterPorNome(string nome)
        {
          return  await _db.Funcionarios.Where(x=> x.Nome.Contains(nome)).AsNoTracking().ToListAsync();
        }
    }
}
