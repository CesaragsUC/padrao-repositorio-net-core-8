using back_end.DTO;

namespace back_end.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task Adicionar(FuncionarioAddDTO funcionario);
        Task Remove(int id);
        Task Atualizar(FuncionarioUpdateDTO funcionario);
        Task<FuncionarioDTO> Obter(int id);
        Task<IEnumerable<FuncionarioDTO>> ObterTodos();
        Task AtualizarLogin(FuncionarioAtualizarLoginDTO funcionario);
        Task<List<FuncionarioDTO>> ObterPorNome(string nome);

    }
}
