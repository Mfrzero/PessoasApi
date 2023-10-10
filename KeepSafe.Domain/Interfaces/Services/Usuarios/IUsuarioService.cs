using KeepSafe.Domain.Models.Usuarios;

namespace KeepSafe.Domain.Interfaces.Services.Usuarios
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuario(string nome, string senha);
        Usuario GetUsuarioById(int id);
        Usuario CreateUsuario(Usuario person);
    }
}
