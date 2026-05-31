namespace Descargar_CFDIS.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string RFC { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public byte[] PfxFile { get; set; }
        public string PasswordKeyEncrypted { get; set; } = string.Empty;
        public string PassUser { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaFin { get; set; }
        public string NoCertificado { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
