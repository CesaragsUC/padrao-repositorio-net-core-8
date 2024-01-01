using back_end.Configuracao;
using Domain.Entidades;

namespace back_end.DTO
{
    public class FuncionarioAtualizarLoginDTO : IMapFrom<Funcionario>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
