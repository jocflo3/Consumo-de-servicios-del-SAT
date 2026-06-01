using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Models;

namespace Descargar_CFDIS.Interfaces.Service
{
    public interface ISatActionsService
    {
        Task GeneraYRegistrarPfx(RegisterCertificateDTO user, int id);
        Task<string> AuthenticateAsync(int id);
        Task<string> GeneraSolicitudUUID(int id, string UUID, string token);
        Task<string> VerificaSolicitud(int id, string IdSolicitud, string token);
    }
}
