namespace Descargar_CFDIS.DTOS
{
    public class UsuarioCreateDTO
    {
        public string RFC { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public string PasswordKey { get; set; } = string.Empty;
    }
}
