using back_end.Configuracao;
using Domain.Entidades;

namespace back_end.DTO
{
    public class FuncionarioAddDTO : IMapFrom<Funcionario>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
