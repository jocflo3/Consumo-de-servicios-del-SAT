using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Interfaces;
using Descargar_CFDIS.Interfaces.Repository;
using Descargar_CFDIS.Models;
using Descargar_CFDIS.Excepciones;
using Descargar_CFDIS.SAT.Security;
namespace Descargar_CFDIS.Services
{
    public class UsuarioService:IUsuarioService
    {
        public readonly IUsuarioRepositoy _usuarioRepository;
        public readonly PasswordProtector _passwordprotect;

        public UsuarioService(IUsuarioRepositoy usuarioRepository, PasswordProtector passwordprotect) 
        {
            _usuarioRepository = usuarioRepository;
            _passwordprotect = passwordprotect;
        }
        public async Task RegistrarUsuario(UsuarioCreateDTO user)
        {
            var usuario = new Usuario
            {
                RFC = user.RFC,
                Nombre = user.Nombre,
                PassUser = BCrypt.Net.BCrypt.HashPassword(user.PasswordKey)
            };
            await _usuarioRepository.RegistrarUsuario(usuario);
        }
        public async Task<List<UsuarioResponseDTO>> ObtenerUsuarios()
        {
            var Usuarios = await _usuarioRepository.ObtenerUsuarios();
            if (Usuarios == null)
            {
                throw new EmptyException("No se encontro información");
            }
            return Usuarios.Select(u => new UsuarioResponseDTO
            {
                Id = u.Id,
                RFC = u.RFC,
                Nombre = u.Nombre,
                Activo = u.Activo,
                FechaRegistro = u.FechaRegistro
            }).ToList();
        }
        public async Task<UsuarioResponseDTO> ObtenerUsuario(int user)
        {
            var Usuario = await _usuarioRepository.ObtenerUsuario(user);
            if (Usuario == null)
            {
                throw new EmptyException("No se encontro información");
            }
            var UsuarioResponse = new UsuarioResponseDTO
            {
                Id = Usuario.Id,
                RFC = Usuario.RFC,
                Nombre = Usuario.Nombre,
                Activo = Usuario.Activo,
                FechaRegistro = Usuario.FechaRegistro
            };
            return UsuarioResponse;
        }
        public async Task ActualizarUsuario(UsuarioUpdateDTO user, int userid)
        {
            var Usuario = new Usuario
            {
                Id = userid,
                Nombre = user.Nombre,
                Activo = user.Activo
            };
            var val = await _usuarioRepository.ActualizarUsuario(Usuario);
            if (val == 0) 
            {
                throw new NotFoundException("Usuario", userid);
            };
        }
        public async Task EliminarUsuario(int user)
        {
            var val = await _usuarioRepository.EliminarUsuario(user);
            if (val == 0) 
            {
                throw new NotFoundException("Usuario", user);
            }
            ;
        }
    }
}
