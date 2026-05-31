namespace Descargar_CFDIS.DTOS
{
    public class UsuarioUpdateDTO
    {
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
        public IFormFile PfxFile { get; set; }
    }
}
