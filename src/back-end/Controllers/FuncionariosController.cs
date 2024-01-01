using back_end.DTO;
using back_end.Services.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : Controller
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly ILogger<FuncionariosController> _logger;


        public FuncionariosController(ILogger<FuncionariosController> logger,
            IFuncionarioService funcionarioService)
        {
            _logger = logger;
            _funcionarioService = funcionarioService;
   
        }

        [HttpGet]
        [Route("listar-funconarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarFuncionarios()
        {
            var funcionarios = await _funcionarioService.ObterTodos();
            return Ok(funcionarios);
        }

        [HttpGet]
        [Route("obter-por-id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var funcionario = await _funcionarioService.Obter(id);
            return Ok(funcionario);
        }

        [HttpGet]
        [Route("obter-por-nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtePorNome(string nome)
        {
            var funcionario = await _funcionarioService.ObterPorNome(nome);
            return Ok(funcionario);
        }

        [HttpPost]
        [Route("cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Cadastrar(FuncionarioAddDTO model)
        {
            await _funcionarioService.Adicionar(model);
            return Created("Funcionario cadastrado com sucesso",null);
        }

        [HttpPut]
        [Route("atualizar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(FuncionarioUpdateDTO model)
        {
            await _funcionarioService.Atualizar(model);
            return Created("Funcionario atualizado com sucesso", null);
        }


        [HttpPut]
        [Route("atualizar-login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarLogin(FuncionarioAtualizarLoginDTO model)
        {
            await _funcionarioService.AtualizarLogin(model);
            return Created("Dados atualizado com sucesso", null);

        }

        [HttpDelete]
        [Route("excluir/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Excluir(int id)
        {

            await _funcionarioService.Remove(id);
            return Ok("Funcionario foi excluido com sucesso");

        }
    }

}
