using KeepSafe.Domain.Interfaces.Services.Pessoas;
using KeepSafe.Domain.Models.Pessoas;
using KeepSafe.Infrastructure.Services.Pessoas;

namespace KeepSafe.Application.Services.Pessoas
{
    public class PessoaService : IPessoaService
    {
        private readonly PessoasDB _db;

        public PessoaService(PessoasDB pessoasDb)
        {
            this._db = pessoasDb;
        }

        public IEnumerable<Pessoa> GetAllPessoas()
        {
            return _db.dadosPessoa;
        }

        public Pessoa GetPessoaById(int id)
        {
            return _db.dadosPessoa.FirstOrDefault(p => p.Id == id);
        }

        public Pessoa CreatePessoa(Pessoa pessoa)
        {
            pessoa.Id = _db.dadosPessoa.Count + 1;
            _db.dadosPessoa.Add(pessoa);
            return pessoa;
        }

        public Pessoa UpdatePessoa(int id, Pessoa pessoa)
        {
            var pessoaExistente = _db.dadosPessoa.FirstOrDefault(p => p.Id == id);
            if (pessoaExistente != null)
            {
                pessoaExistente.Nome = pessoa.Nome;
                pessoaExistente.Cidade = pessoa.Cidade;
                pessoaExistente.Idade = pessoa.Idade;
                pessoaExistente.Sexo = pessoa.Sexo;
            }
            return pessoaExistente;
        }

        public void DeletePessoa(int id)
        {
            var pessoa = _db.dadosPessoa.FirstOrDefault(p => p.Id == id);
            if (pessoa != null)
            {
                _db.dadosPessoa.Remove(pessoa);
            }
        }
    }
}
