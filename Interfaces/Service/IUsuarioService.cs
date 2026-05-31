using Descargar_CFDIS.DTOS;

namespace Descargar_CFDIS.Interfaces
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(UsuarioCreateDTO user);
        Task<List<UsuarioResponseDTO>> ObtenerUsuarios();
        Task<UsuarioResponseDTO> ObtenerUsuario(int user);
        Task ActualizarUsuario(UsuarioUpdateDTO user, int userid);
        Task EliminarUsuario(int user);
    }
}
