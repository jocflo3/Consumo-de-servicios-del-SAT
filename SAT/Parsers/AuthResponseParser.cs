using Descargar_CFDIS.Excepciones;
using System.Xml;

namespace Descargar_CFDIS.SAT.Parsers
{
    public class AuthResponseParser
    {
        public string ExtractToken(string responseXml)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(responseXml);

            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);

            ns.AddNamespace( "a", "http://DescargaMasivaTerceros.gob.mx");

            XmlNode? tokenNode = doc.SelectSingleNode( "//a:AutenticaResult", ns);

            if (tokenNode == null)
            {
                throw new Exception(
                    "No se encontró el token SAT."
                );
            }

            return tokenNode.InnerText;
        }
        public string ExtractIdSolicitud(string xml)
        {
            XmlDocument doc = new();

            doc.LoadXml(xml);

            XmlNode node = doc.GetElementsByTagName("SolicitaDescargaFolioResult")[0];

            return node?.Attributes?["IdSolicitud"]?.Value ?? throw new Exception("No se encontró IdSolicitud");
        }
        public byte[] ExtractZipBytes(string xmlResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);

            XmlNode? paqueteNode = doc.GetElementsByTagName("Paquete").Cast<XmlNode>().FirstOrDefault();

            if (paqueteNode == null)
            {
                throw new EmptyException("No se encontró el paquete.");
            }

            return Convert.FromBase64String(paqueteNode.InnerText);
        }
        public async Task<string> GuardarZipAsync(byte[] zipBytes,string carpetaDestino)
        {
            Directory.CreateDirectory(carpetaDestino);

            string nombreArchivo = $"{Guid.NewGuid()}.zip";

            string rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

            await File.WriteAllBytesAsync(rutaCompleta,zipBytes);

            return rutaCompleta;
        }
    }
}