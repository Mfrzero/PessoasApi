using KeepSafe.Api.Controllers;
using KeepSafe.Domain.Interfaces.Services.Usuarios;
using KeepSafe.Domain.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KeepSafe.Api.UnitTests.Controllers.Usuarios
{
    public class UsuarioControllerTests
    {
        [Fact]
        public void GetAll_ReturnsOkResultWithListOfUsuarios()
        {
            // Arrange
            var usuarioServiceMock = new Mock<IUsuarioService>();
            usuarioServiceMock.Setup(service => service.GetAllUsuarios())
                .Returns(new List<Usuario>());

            var controller = new UsuarioController(usuarioServiceMock.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var usuarios = Assert.IsAssignableFrom<IEnumerable<Usuario>>(okResult.Value);
            Assert.Empty(usuarios);
        }

        [Fact]
        public async Task CreateUsuario_ReturnsCreatedAtActionResultWithNewUsuario()
        {
            // Arrange
            var newUsuario = new Usuario { Nome = "NewUser", Senha = "NewPass", Role = "admin" };
            var createdUsuario = new Usuario { Id = 1, Nome = "NewUser", Senha = "NewPass", Role = "user" };

            var usuarioServiceMock = new Mock<IUsuarioService>();
            usuarioServiceMock.Setup(service => service.CreateUsuario(newUsuario))
                .Returns(createdUsuario);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            // Act
            var result = await controller.CreateUsuario(newUsuario);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedUsuario = Assert.IsAssignableFrom<Usuario>(createdAtActionResult.Value);
            Assert.Equal(createdUsuario.Id, returnedUsuario.Id);
        }

        [Fact]
        public void GetUsuarioById_ExistingId_ReturnsOkResultWithUsuario()
        {
            // Arrange
            int userId = 1;
            var usuario = new Usuario { Id = userId, Nome = "TestUser", Senha = "TestPass", Role = "user" };

            var usuarioServiceMock = new Mock<IUsuarioService>();
            usuarioServiceMock.Setup(service => service.GetUsuarioById(userId))
                .Returns(usuario);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            // Act
            var result = controller.GetUsuarioById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsuario = Assert.IsAssignableFrom<Usuario>(okResult.Value);
            Assert.Equal(usuario.Id, returnedUsuario.Id);
        }

        [Fact]
        public void GetUsuarioById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int userId = 123;

            var usuarioServiceMock = new Mock<IUsuarioService>();
            usuarioServiceMock.Setup(service => service.GetUsuarioById(userId))
                .Returns((Usuario)null);

            var controller = new UsuarioController(usuarioServiceMock.Object);

            // Act
            var result = controller.GetUsuarioById(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //[Fact]
        //public async Task Post_ValidUsuario_ReturnsToken()
        //{
        //    // Arrange
        //    var usuarioServiceMock = new Mock<IUsuarioService>();
        //    var usuario = new Usuario { Nome = "TestUser", Senha = "TestPass" };
        //    var expectedToken = "generated_token";

        //    usuarioServiceMock.Setup(service => service.GetUsuario(usuario.Nome, usuario.Senha))
        //        .Returns(new Usuario { Id = 1, Nome = "TestUser", Senha = "TestPass", Role = "user" });

        //    var controller = new UsuarioController(usuarioServiceMock.Object);

        //    // Act
        //    var result = await controller.Post(usuario);

        //    // Assert
        //    var okResult = Assert.IsType<ActionResult<object>>(result);
        //    var returnedToken = Assert.IsAssignableFrom<string>(okResult.Value);
        //    Assert.(expectedToken, returnedToken);
        //}

        //[Fact]
        //public async Task Post_InvalidUsuario_ReturnsNotFoundResultWithMessage()
        //{
        //    // Arrange
        //    var usuarioServiceMock = new Mock<IUsuarioService>();
        //    var usuario = new Usuario { Nome = "InvalidUser", Senha = "InvalidPass" };

        //    usuarioServiceMock.Setup(service => service.GetUsuario(usuario.Nome, usuario.Senha))
        //        .Returns((Usuario)null);

        //    var controller = new UsuarioController(usuarioServiceMock.Object);

        //    // Act
        //    var result = await controller.Post(usuario);

        //    // Assert
        //    var notFoundResult = Assert.IsType<ActionResult<object>>(result);
        //    var response = Assert.IsAssignableFrom<object>(notFoundResult);
        //    Assert.Equal("nome ou senha invalidos", response.GetType().GetProperty("message")?.GetValue(response));
        //}
    }
}
