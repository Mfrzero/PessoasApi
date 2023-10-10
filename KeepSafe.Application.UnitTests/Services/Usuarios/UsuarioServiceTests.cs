using KeepSafe.Application.Services.Usuarios;
using KeepSafe.Domain.Interfaces.Services.Usuarios;
using KeepSafe.Domain.Models.Usuarios;
using KeepSafe.Infrastructure.Services.Usuarios;
using Xunit;

namespace KeepSafe.Api.UnitTests.Services.Usuarios
{
    public class UsuarioServiceTests
    {
        public readonly UsuariosDB _db;

        private readonly IUsuarioService _usuarioService;
        public UsuarioServiceTests()
        {
            _db = new UsuariosDB();
            _usuarioService = new UsuarioService(_db);
        }
        [Fact]
        public void CreateUsuario_AddsUsuarioToList()
        {
            // Arrange
            var service = new UsuarioService(_db);
            var usuario = new Usuario { Nome = "TestUser", Senha = "TestPass", Role = "user" };

            // Act
            var result = service.CreateUsuario(usuario);

            // Assert
            Assert.Equal(usuario, result);
            Assert.Single(_db.dadosUsuario);
            Assert.Equal(usuario.Id, _db.dadosUsuario.First().Id);
        }

        [Fact]
        public void GetAllUsuarios_ReturnsAllUsuarios()
        {
            // Arrange
            var service = new UsuarioService(_db);
            _db.dadosUsuario.AddRange(new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "User1", Senha = "Pass1", Role = "user" },
                new Usuario { Id = 2, Nome = "User2", Senha = "Pass2", Role = "admin" }
            });

            // Act
            var result = service.GetAllUsuarios();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetUsuario_ReturnsMatchingUsuario()
        {
            // Arrange
            var service = new UsuarioService(_db);
            _db.dadosUsuario.Add(new Usuario { Id = 1, Nome = "User1", Senha = "Pass1", Role = "user" });

            // Act
            var result = service.GetUsuario("User1", "Pass1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User1", result.Nome);
        }

        [Fact]
        public void GetUsuarioById_ReturnsMatchingUsuario()
        {
            // Arrange
            var service = new UsuarioService(_db);
            _db.dadosUsuario.Add(new Usuario { Id = 1, Nome = "User1", Senha = "Pass1", Role = "user" });

            // Act
            var result = service.GetUsuarioById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User1", result.Nome);
        }

        // Additional tests can be written for edge cases and further validation.
    }
}
