using KeepSafe.Domain.Models.Pessoas;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xunit;

namespace KeepSafe.Api.UnitTests.Controllers.Pessoas
{
    public class PessoaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE2OTE2MTI3MDYsImV4cCI6MTY5MTYxNjMwNiwiaWF0IjoxNjkxNjEyNzA2fQ.LzuMqK7e4tnoT8HKF-Rvc52F5CTMBIWwbAvCFycKHcM";
        public PessoaControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetAll_WithValidToken_ReturnsOkResultWithPessoas()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act
            var response = await client.GetAsync("/api/pessoas");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var pessoas = JsonSerializer.Deserialize<IEnumerable<Pessoa>>(content);

            Assert.NotNull(pessoas);
        }


        [Fact]
        public async Task GetAll_WithoutToken_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/pessoas");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CreatePessoa_WithValidToken_ReturnsCreatedResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var pessoa = new Pessoa
            {
                Id = 1,
                Nome = "Matheus",
                Cidade = "Santos",
                Idade = 25,
                Sexo = 0
            };

            var content = new StringContent(JsonSerializer.Serialize(pessoa), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/pessoas", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdPessoa = JsonSerializer.Deserialize<Pessoa>(responseContent);

            Assert.NotNull(createdPessoa);
        }

        [Fact]
        public async Task CreatePessoa_WithBadRequest_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var invalidPessoa = new Pessoa
            {
                Id = 0,
                Nome = "",
                Cidade = "",
                Idade = 0,
                Sexo = Domain.Enums.TipoSexo.FEMININO
            };

            var content = new StringContent(JsonSerializer.Serialize(invalidPessoa), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/pessoas", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetPessoaById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var invalidId = -1;

            // Act
            var response = await client.GetAsync($"/api/pessoas/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
