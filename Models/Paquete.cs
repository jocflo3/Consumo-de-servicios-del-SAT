namespace Descargar_CFDIS.Models
{
    public class Paquete
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public string PackageIdSAT { get; set; } = string.Empty;
        public byte[]? ZipContenido { get; set; }
        public bool Descargado { get; set; }
        public DateTime? FechaDescarga { get; set; }
    }
}
