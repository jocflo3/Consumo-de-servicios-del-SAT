namespace Descargar_CFDIS.Excepciones
{
    public class ForbiddenException: BusinessException
    {
        public ForbiddenException(string mensaje = "Acceso denegado")
            : base(
                mensaje,
                403,
                "FORBIDDEN")
        {
        }
    }
}
