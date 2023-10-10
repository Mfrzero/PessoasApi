using KeepSafe.Api.Controllers;
using KeepSafe.Domain.Enums;
using KeepSafe.Domain.Interfaces.Services.Pessoas;
using KeepSafe.Domain.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KeepSafe.Api.UnitTests.Controllers.Pessoas
{
    public class PessoaControllerTests
    {
        [Fact]
        public void CreatePessoa_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var pessoaServiceMock = new Mock<IPessoaService>();
            var pessoa = new Pessoa { Nome = "TestPerson", Idade = 30, Sexo = TipoSexo.MASCULINO };
            pessoaServiceMock.Setup(service => service.CreatePessoa(It.IsAny<Pessoa>())).Returns(pessoa);
            var controller = new PessoaController(pessoaServiceMock.Object);

            // Act
            var result = controller.CreatePessoa(pessoa);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPessoa = Assert.IsAssignableFrom<Pessoa>(createdAtActionResult.Value);
            Assert.Equal(pessoa.Nome, returnedPessoa.Nome);
        }

        [Fact]
        public void GetAll_ReturnsListOfPessoas()
        {
            // Arrange
            var pessoaServiceMock = new Mock<IPessoaService>();
            pessoaServiceMock.Setup(service => service.GetAllPessoas()).Returns(new List<Pessoa>());
            var controller = new PessoaController(pessoaServiceMock.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var pessoas = Assert.IsAssignableFrom<IEnumerable<Pessoa>>(okResult.Value);
            Assert.Empty(pessoas);
        }

        [Fact]
        public void GetPessoaById_ExistingId_ReturnsOkResultWithPessoa()
        {
            // Arrange
            var pessoaServiceMock = new Mock<IPessoaService>();
            var pessoa = new Pessoa { Id = 1, Nome = "TestPerson", Idade = 30, Sexo = TipoSexo.MASCULINO };
            pessoaServiceMock.Setup(service => service.GetPessoaById(1)).Returns(pessoa);
            var controller = new PessoaController(pessoaServiceMock.Object);

            // Act
            var result = controller.GetPessoaById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPessoa = Assert.IsAssignableFrom<Pessoa>(okResult.Value);
            Assert.Equal(pessoa.Id, returnedPessoa.Id);
        }

        [Fact]
        public void GetPessoaById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var pessoaServiceMock = new Mock<IPessoaService>();
            pessoaServiceMock.Setup(service => service.GetPessoaById(It.IsAny<int>())).Returns((Pessoa)null);
            var controller = new PessoaController(pessoaServiceMock.Object);

            // Act
            var result = controller.GetPessoaById(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }


        [Fact]
        public void UpdatePessoa_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var pessoaServiceMock = new Mock<IPessoaService>();
            pessoaServiceMock.Setup(service => service.UpdatePessoa(It.IsAny<int>(), It.IsAny<Pessoa>())).Returns((Pessoa)null);
            var controller = new PessoaController(pessoaServiceMock.Object);

            // Act
            var result = controller.UpdatePessoa(new Pessoa(), 99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeletePessoa_ExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var pessoaServiceMock = new Mock<IPessoaService>();
            var controller = new PessoaController(pessoaServiceMock.Object);

            // Act
            var result = controller.DeletePessoa(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
