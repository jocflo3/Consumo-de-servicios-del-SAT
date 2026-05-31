namespace Descargar_CFDIS.Models
{
    public class SolicitudSAT
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? RequestIdSAT { get; set; }
        public string EstadoSolicitud { get; set; } = string.Empty;
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? TipoSolicitud { get; set; }
        public string? UUIDSolicitado { get; set; }
        public string? CodEstadoSAT { get; set; }
        public string? MensajeSAT { get; set; }
    }
}
