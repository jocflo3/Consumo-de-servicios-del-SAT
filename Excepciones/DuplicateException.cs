using Descargar_CFDIS.Helpers;

namespace Descargar_CFDIS.Excepciones
{
    public class DuplicateException : BusinessException
    {
        public string EntityName { get; }

        public object EntityId { get; }

        public DuplicateException(string Entidad, object Clave, bool esFemenino = false)
            : base($"{Entidad} {Clave} ya {ConditionalHelper.Select(esFemenino, "registrada", "registrado")}",
                409,
                "DUPLICATED")
        {
            EntityName = Entidad;
            EntityId = Clave;
        }
    }
}
