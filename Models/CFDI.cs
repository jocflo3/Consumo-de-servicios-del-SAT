namespace DescargaR_CFDIS.Models
{
    public class CFDI
    {
        public int Id { get; set; }
        public string UUID { get; set; } = string.Empty;
        public string? RFCEmisor { get; set; }
        public string? RFCReceptor { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal? Total { get; set; }
        public string? TipoComprobante { get; set; }
        public string? EstadoComprobante { get; set; }
        public string? RutaXML { get; set; }
        public string? XmlContenido { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
