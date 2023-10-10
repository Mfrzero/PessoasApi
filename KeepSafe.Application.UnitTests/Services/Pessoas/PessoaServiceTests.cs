using KeepSafe.Application.Services.Pessoas;
using KeepSafe.Domain.Enums;
using KeepSafe.Domain.Interfaces.Services.Pessoas;
using KeepSafe.Domain.Models.Pessoas;
using KeepSafe.Infrastructure.Services.Pessoas;
using Xunit;

namespace KeepSafe.Api.UnitTests.Services.Pessoas
{
    public class PessoaServiceTests
    {
        private readonly IPessoaService _pessoaService;
        private readonly PessoasDB _Db;
        public PessoaServiceTests()
        {
            _Db = new PessoasDB();
            _pessoaService = new PessoaService(_Db);
        }
        [Fact]
        public void GetAllPessoas_ShouldReturnAllPessoas()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "Alice", Cidade = "CityA", Idade = 25, Sexo = TipoSexo.FEMININO },
                new Pessoa { Id = 2, Nome = "Bob", Cidade = "CityB", Idade = 30, Sexo = TipoSexo.MASCULINO }
            };
            _Db.dadosPessoa.AddRange(pessoas);

            // Act
            var result = _pessoaService.GetAllPessoas();

            // Assert
            Assert.Equal(pessoas, result);
        }

        [Fact]
        public void GetPessoaById_ShouldReturnCorrectPessoa()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "Alice", Cidade = "CityA", Idade = 25, Sexo = TipoSexo.FEMININO },
                new Pessoa { Id = 2, Nome = "Bob", Cidade = "CityB", Idade = 30, Sexo =  TipoSexo.MASCULINO }
            };
            _Db.dadosPessoa.AddRange(pessoas);

            // Act
            var result = _pessoaService.GetPessoaById(2);

            // Assert
            Assert.Equal(pessoas[1], result);
        }

        [Fact]
        public void CreatePessoa_ShouldAddNewPessoa()
        {
            // Arrange
            var newPessoa = new Pessoa { Nome = "Charlie", Cidade = "CityC", Idade = 28, Sexo = TipoSexo.MASCULINO };

            // Act
            var result = _pessoaService.CreatePessoa(newPessoa);

            // Assert
            Assert.Equal(newPessoa, result);
            Assert.Contains(newPessoa, _Db.dadosPessoa);
        }

        public void UpdatePessoa_ShouldUpdateExistingPessoa()
        {
            // Arrange
            var pessoaId = 1;
            var pessoaExistente = new Pessoa { Id = pessoaId, Nome = "Alice", Cidade = "CityA", Idade = 25, Sexo = TipoSexo.FEMININO };
            _Db.dadosPessoa.Add(pessoaExistente);

            var updatedPessoa = new Pessoa { Id = pessoaId, Nome = "UpdatedAlice", Cidade = "UpdatedCity", Idade = 30, Sexo = TipoSexo.FEMININO };

            // Act
            var result = _pessoaService.UpdatePessoa(pessoaId, updatedPessoa);

            // Assert
            Assert.Equal(updatedPessoa, pessoaExistente);
            Assert.Equal(updatedPessoa, result);
        }

        [Fact]
        public void DeletePessoa_ShouldRemoveExistingPessoa()
        {
            // Arrange
            var pessoaId = 1;
            var pessoaExistente = new Pessoa { Id = pessoaId, Nome = "Alice", Cidade = "CityA", Idade = 25, Sexo = TipoSexo.FEMININO };
            _Db.dadosPessoa.Add(pessoaExistente);

            // Act
            _pessoaService.DeletePessoa(pessoaId);

            // Assert
            Assert.Empty(_Db.dadosPessoa);
        }
    }
}
