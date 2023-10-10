using KeepSafe.Domain.Models.Usuarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace KeepSafe.Api.IntegrationTests.Controllers.Usuarios
{
    public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public UsuarioControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsListOfUsuarios()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/api/usuario");

            // Assert
            response.EnsureSuccessStatusCode();
            var usuarios = JsonConvert.DeserializeObject<Usuario[]>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(usuarios);
        }

        [Fact]
        public async Task CreateUsuario_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var usuario = new Usuario { Nome = "TestUser", Senha = "TestPass", Role = "user" };
            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/usuario", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains("/api/usuario/", response.Headers.Location.ToString());
        }

        [Fact]
        public async Task GetUsuarioById_NonExistingId_ReturnsNotFound()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/api/usuario/999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
