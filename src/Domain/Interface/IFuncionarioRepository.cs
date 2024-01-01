using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Task<List<Funcionario>> ObterPorNome(string nome);
    }
}
