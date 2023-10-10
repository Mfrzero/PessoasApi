using KeepSafe.Application.Token;
using KeepSafe.Domain.Interfaces.Services.Usuarios;
using KeepSafe.Domain.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace KeepSafe.Api.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Usuario>> GetAll()
        {
            var result = _usuarioService.GetAllUsuarios();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> CreateUsuario([FromBody] Usuario usuario)
        {
            var result = _usuarioService.CreateUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = result.Id }, result);

        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuarioById(int id)
        {
            var result = _usuarioService.GetUsuarioById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("adicionaUsuarios")]
        public IActionResult PostUsers()
        {
            _usuarioService.CreateUsuario(new Usuario { Nome = "Matheus", Senha = "12345", Role = "admin" });
            _usuarioService.CreateUsuario(new Usuario { Nome = "Mahalo", Senha = "54321", Role = "user" });
            return Ok();
        }
        
        [HttpPost]
        [Route("geraToken")]
        public async Task<ActionResult<dynamic>> Post([FromBody] Usuario usuario)
        {
            var user = _usuarioService.GetUsuario(usuario.Nome, usuario.Senha);
            if (user == null)
                return NotFound(new { message = "nome ou senha invalidos" });

            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return token;
        }
    }
}
