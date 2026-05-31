namespace Descargar_CFDIS.Excepciones
{
    public class ValidationException: BusinessException
    {
        public ValidationException(string mensaje = "Datos inválidos")
            : base(
                  mensaje,
                  400,
                  "VALIDATION_ERROR")
        {
        }
    }
}
