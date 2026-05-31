namespace Descargar_CFDIS.DTOS
{
    public class CFDIResponseDTO
    {
        public string UUID { get; set; } = string.Empty;
        public string? RFCEmisor { get; set; }
        public string? RFCReceptor { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal? Total { get; set; }
        public string? TipoComprobante { get; set; }
    }
}
