using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Descarga_CFDI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _userService;

        public UsuarioController(IUsuarioService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistraUsuario(UsuarioCreateDTO user)
        {

            await _userService.RegistrarUsuario(user);
            return Ok("Registrado correctamente");
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuario()
        {
            var Usuarios = await _userService.ObtenerUsuarios();
            return Ok(Usuarios);
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> EliminaUsuario(int userid)
        {
            await _userService.EliminarUsuario(userid);
            return Ok("Usuario eliminado correctamente");
        }
        [HttpPut("{idUser}")]
        public async Task<IActionResult> ActualizarUsuario(int idUser, UsuarioUpdateDTO user)
        {
            await _userService.ActualizarUsuario(user, idUser);
            return Ok("Usuario actualizado correctamente");
        }
    }
    
}
