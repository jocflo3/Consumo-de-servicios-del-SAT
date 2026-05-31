namespace Descargar_CFDIS.DTOS
{
    public class SolicitudResponseDTO
    {
        public int Id { get; set; }
        public string? RequestIdSAT { get; set; }
        public string EstadoSolicitud { get; set; } = string.Empty;
        public string? UUIDSolicitado { get; set; }
        public string? CodEstadoSAT { get; set; }
        public string? MensajeSAT { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}
