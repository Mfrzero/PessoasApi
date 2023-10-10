using KeepSafe.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace KeepSafe.Domain.Models.Pessoas
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Sexo é obrigatório.")]
        public TipoSexo? Sexo { get; set; }
        [Required(ErrorMessage = "Idade é obrigatória.")]
        [Range(0, 150, ErrorMessage = "Idade não pode ser menor que 0 nem maior que 150.")]
        public int Idade { get; set; }
        [Required(ErrorMessage = "Cidade é obrigatória.")]
        [MaxLength(100, ErrorMessage = "Cidade não pode ter mais de 100 caracteres.")]
        public string Cidade { get; set; }
    }
}
