using KeepSafe.Domain.Models.Pessoas;

namespace KeepSafe.Domain.Interfaces.Services.Pessoas
{
    public interface IPessoaService
    {
        IEnumerable<Pessoa> GetAllPessoas();
        Pessoa GetPessoaById(int id);
        Pessoa CreatePessoa(Pessoa person);
        Pessoa UpdatePessoa(int id, Pessoa person);
        void DeletePessoa(int id);

    }
}
