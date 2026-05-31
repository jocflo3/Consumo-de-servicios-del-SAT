namespace Descargar_CFDIS.DTOS
{
    public class RegisterCertificateDTO
    {
        public IFormFile CerFile { get; set; }

        public IFormFile KeyFile { get; set; }

        public string Password { get; set; }
    }
}
