namespace Descargar_CFDIS.DTOS
{
    public class UsuarioResponseDTO
    {
        public int Id { get; set; }
        public string RFC { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
