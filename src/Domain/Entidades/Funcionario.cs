using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Funcionario : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
