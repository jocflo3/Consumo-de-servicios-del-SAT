using Descargar_CFDIS.Helpers;

namespace Descargar_CFDIS.Excepciones
{
    public class EmptyException : BusinessException
    {

        public EmptyException(string Mensaje="") : base(Mensaje,
                200,
                "NOT_DATA")
        {
        }
    }
}
