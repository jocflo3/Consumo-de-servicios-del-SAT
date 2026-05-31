using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Models;
using Org.BouncyCastle.Utilities;

namespace Descargar_CFDIS.Interfaces.Repository
{
    public interface ISatActionsRepository
    {

        Task<Usuario> ObtenerPfxPass(int user);
        Task<int> GeneraYRegistrarPfx(byte[] pfx, string pass, int id);
    }
}
