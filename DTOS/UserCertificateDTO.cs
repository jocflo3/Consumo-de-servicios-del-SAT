namespace Descargar_CFDIS.DTOS
{
    public class UserCertificateDTO
    {
        public int Id { get; set; }

        public byte[]? PfxFile { get; set; }
        public string? ProtectedPassword { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
