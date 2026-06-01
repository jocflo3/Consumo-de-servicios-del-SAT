namespace Descargar_CFDIS.SAT.DTO
{
    public class ConfigSolicitudMasivoDTO
    {
        public string TipoPeticion { get; set; }//CF,MT
        public string? TipoCFDI {  get; set; }//E,R
        public string? EstadoCFDI { get; set; }//V,C
        public DateTime? FechaIni {  get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
