using KeepSafe.Domain.Interfaces.Services.Pessoas;
using KeepSafe.Domain.Models.Pessoas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepSafe.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/pessoas")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Authorize(Roles = "admin,user")]
        [HttpPost("adiciona_varias_pessoas")]
        public async Task<ActionResult<int>> CreateVariasPessoa(Pessoa pessoa)
        {
            _pessoaService.CreatePessoa(new Pessoa() { Id = 1, Cidade = "SANTOS", Idade = 25, Nome = "MATHEUS", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 2, Cidade = "SAO VICENTE", Idade = 28, Nome = "NICOLAS", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 3, Cidade = "GUARUJA", Idade = 35, Nome = "NOELE", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 4, Cidade = "PRAIA GRANDE", Idade = 45, Nome = "NICOLE", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 5, Cidade = "SANTOS", Idade = 29, Nome = "MARCIA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 6, Cidade = "GUARUJA", Idade = 30, Nome = "MOHAMED", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 7, Cidade = "BH", Idade = 28, Nome = "KEVIN", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 8, Cidade = "BH", Idade = 84, Nome = "VALESVSKA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 9, Cidade = "SAO VICENTE", Idade = 62, Nome = "JOAO", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 10, Cidade = "SANTOS", Idade = 45, Nome = "JOANA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 11, Cidade = "SANTOS", Idade = 33, Nome = "ALEX", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 12, Cidade = "BH", Idade = 20, Nome = "MARCO", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 13, Cidade = "SANTOS", Idade = 38, Nome = "DAIANA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 14, Cidade = "BH", Idade = 14, Nome = "EVELIN", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 15, Cidade = "SANTOS", Idade = 25, Nome = "MATHEUS", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 16, Cidade = "BH", Idade = 25, Nome = "MATHEUS", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 17, Cidade = "SAO VICENTE", Idade = 25, Nome = "MATHEUS", Sexo = Domain.Enums.TipoSexo.MASCULINO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 18, Cidade = "GUARUJA", Idade = 38, Nome = "DAIANA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 19, Cidade = "BH", Idade = 38, Nome = "DAIANA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 20, Cidade = "SAO VICENTE", Idade = 38, Nome = "DAIANA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            _pessoaService.CreatePessoa(new Pessoa() { Id = 21, Cidade = "PRAIA GRANDE", Idade = 38, Nome = "DAIANA", Sexo = Domain.Enums.TipoSexo.FEMININO });
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult<Pessoa> CreatePessoa([FromBody] Pessoa pessoa)
        {
            var result = _pessoaService.CreatePessoa(pessoa);
            return CreatedAtAction(nameof(GetPessoaById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet()]
        public ActionResult<IEnumerable<Pessoa>> GetAll()
        {
            var result = _pessoaService.GetAllPessoas();
            return Ok(result);
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public ActionResult<Pessoa> GetPessoaById(int id)
        {
            var result = _pessoaService.GetPessoaById(id);
            if (result == null)
                return NotFound(new { message = "pessoa não encontrada" });

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult UpdatePessoa([FromBody] Pessoa pessoa, int id)
        {
            var result = _pessoaService.UpdatePessoa(id, pessoa);
            if (result == null)
                return NotFound(new { message = "pessoa não encontrada" });

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeletePessoa(int id)
        {
            _pessoaService.DeletePessoa(id);
            return NoContent();
        }
    }
}
