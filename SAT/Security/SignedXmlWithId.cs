using System.Security.Cryptography.Xml;
using System.Xml;

namespace Descargar_CFDIS.SAT.Security
{
    public class SignedXmlWithId : SignedXml
    {

        // =============================================================
        // SIGNED XML CUSTOM
        // =============================================================
        public SignedXmlWithId(XmlDocument document) : base(document)
        {
        }
        public override XmlElement GetIdElement(XmlDocument document, string idValue)
        {
            XmlElement idElem = base.GetIdElement(document, idValue);

            if (idElem != null) return idElem;

            XmlNamespaceManager ns = new XmlNamespaceManager(document.NameTable);
            ns.AddNamespace("u", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            return document.SelectSingleNode($"//*[@u:Id='{idValue}']", ns) as XmlElement;
        }
    }
}
