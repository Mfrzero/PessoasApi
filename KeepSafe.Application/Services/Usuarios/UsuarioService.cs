using KeepSafe.Domain.Interfaces.Services.Usuarios;
using KeepSafe.Domain.Models.Usuarios;
using KeepSafe.Infrastructure.Services.Usuarios;

namespace KeepSafe.Application.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuariosDB _db;

        public UsuarioService(UsuariosDB db)
        {
            _db = db;
        }

        public Usuario CreateUsuario(Usuario usuario)
        {
            usuario.Id = _db.dadosUsuario.Count + 1;
            _db.dadosUsuario.Add(usuario);
            return usuario;
        }

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return _db.dadosUsuario;
        }
        public Usuario GetUsuario(string nome, string senha)
        {
            return _db.dadosUsuario.FirstOrDefault(p => p.Nome == nome);
        }

        public Usuario GetUsuarioById(int id)
        {
            return _db.dadosUsuario.FirstOrDefault(p => p.Id == id);
        }
    }
}
