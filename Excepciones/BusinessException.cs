namespace Descargar_CFDIS.Excepciones
{
    public abstract class BusinessException: Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }
        protected BusinessException(string mensaje, int statusCode, string errorCode) 
            : base(mensaje) 
        { 
            StatusCode = statusCode; 
            ErrorCode = errorCode; 
        }
    }
}
