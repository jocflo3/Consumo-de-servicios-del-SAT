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
    }
}