namespace Descargar_CFDIS.Helpers
{
    public class ObtenerBytes
    {
        public async Task<byte[]> ObtenerBytesAsync(IFormFile file)
        {
            using MemoryStream ms = new();

            await file.CopyToAsync(ms);

            return ms.ToArray();
        }
    }
}
