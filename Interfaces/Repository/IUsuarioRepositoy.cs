using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Models;

namespace Descargar_CFDIS.Interfaces.Repository
{
    public interface IUsuarioRepositoy
    {
        Task RegistrarUsuario(Usuario user);
        Task<List<Usuario>> ObtenerUsuarios();
        Task<Usuario?> ObtenerUsuario(int user);
        Task <int>ActualizarUsuario(Usuario user);
        Task <int>EliminarUsuario(int user);
    }
}
