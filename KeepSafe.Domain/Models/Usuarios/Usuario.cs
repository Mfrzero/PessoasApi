using System.ComponentModel.DataAnnotations;

namespace KeepSafe.Domain.Models.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Nome não pode ter mais de 100 caracteres.")]
        public string Senha { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Role é obrigatório [admin/user].")]
        public string Role { get; set; }
    }
}
