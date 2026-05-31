using System.Xml;
using Descargar_CFDIS.SAT.Constants;

namespace Descargar_CFDIS.SAT.Builders
{
    public class SatSoapBuilder
    {
        public XmlElement CreateEnvelope(XmlDocument doc)
        {
            XmlElement envelope = doc.CreateElement("s", "Envelope", SatNamEspaces.SOAP);
            return envelope;
        }
        public XmlElement CreateHeader(XmlDocument doc)
        {
            XmlElement header = doc.CreateElement("s", "Header", SatNamEspaces.SOAP);
            return header;
        }
        public XmlElement CreateBody(XmlDocument doc)
        {
            XmlElement body = doc.CreateElement("s", "Body", SatNamEspaces.SOAP);
            return body;
        }
        public XmlDocument CreateDoc()
        {
            XmlDocument doc = new XmlDocument();
            // CRÍTICO PARA XMLDSIG
            doc.PreserveWhitespace = true;
            return doc;
        }
    }
}
