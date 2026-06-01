using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Descargar_CFDIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SATController : ControllerBase
    {
        private readonly ISatActionsService _satActions;
        public SATController(ISatActionsService satAct) 
        {
            _satActions = satAct;
        }
        [HttpPost("/Auth")]
        public async Task<IActionResult> Autenticar(int id)
        {
            string token =  await _satActions.AuthenticateAsync(id);

            return Ok(token);
        }
        [HttpPost("/Registrar")]
        public async Task<IActionResult> GenerarSolicitud(int id, DateTime fechaIni,DateTime fechaFin, int Opt)
        {
            return Ok();
        }
        [HttpPost("/UUID")]
        public async Task<IActionResult> GenerarSolicitudUUID(int id, string UUID, string token)
        {
            string resp = await _satActions.GeneraSolicitudUUID(id, UUID, token);
            return Ok();
        }
        [HttpPost("/Verificar")]
        public async Task<IActionResult> VerificarSolicitud(int id, string IdSolicitud, string token)
        {
            string resp = await _satActions.VerificaSolicitud(id, IdSolicitud, token);
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
            await _satActions.GeneraYRegistrarPfx(arch,id);

            return Ok();
        }
    }
}
