using back_end.Configuracao;
using Domain.Entidades;

namespace back_end.DTO
{
    public class FuncionarioDTO : IMapFrom<Funcionario>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
