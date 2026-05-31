using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace Descargar_CFDIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SATController : ControllerBase
    {
        private readonly ISatActionsService _satAuth;
        public SATController(ISatActionsService satAuth) 
        {
            _satAuth = satAuth;
        }
        [HttpPost("/Auth")]
        public async Task<IActionResult> Autenticar(int id)
        {
            string token =  await _satAuth.AuthenticateAsync(id);

            return Ok(token);
        }
        [HttpPost("/Registrar")]
        public async Task<IActionResult> GenerarSolicitud(int id, DateTime fechaIni,DateTime fechaFin, int Opt)
        {
            return Ok();
        }
        [HttpPost("/Verificar")]
        public async Task<IActionResult> VerificarSolicitud(string cveSolicitud)
        {
            return Ok();
        }
        [HttpPost("/Descargar")]
        public async Task<IActionResult> DescargarSolicitud(string cveSolicitud)
        {
            return Ok();
        }
        [HttpPost("/RegistrarPfx")]
        public async Task<IActionResult> RegistrarPfx(RegisterCertificateDTO arch, int id)
        {
            await _satAuth.GeneraYRegistrarPfx(arch,id);

            return Ok();
        }
    }
}
