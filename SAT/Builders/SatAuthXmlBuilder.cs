using Descargar_CFDIS.Services;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Descargar_CFDIS.SAT.Constants;
using Descargar_CFDIS.SAT.Security;

namespace Descargar_CFDIS.SAT.Builders
{
    public class SatAuthXmlBuilder
    {
        private readonly SatSoapBuilder _soapBuilder;

        public SatAuthXmlBuilder()
        {
            _soapBuilder = new SatSoapBuilder();
        }
        public string GenerarXmlAutenticacion(X509Certificate2 cert)
        {
            DateTime created = DateTime.UtcNow;
            DateTime expires = created.AddMinutes(5);

            string uuid = $"uuid-{Guid.NewGuid()}-4";
            byte[] certBytes = cert.Export(X509ContentType.Cert);
            string certBase64 = Convert.ToBase64String(certBytes);

            XmlDocument doc = _soapBuilder.CreateDoc();

            // =====================================================
            // ENVELOPE
            // =====================================================

            XmlElement envelope = _soapBuilder.CreateEnvelope(doc);
            envelope.SetAttribute("xmlns:u", SatNamEspaces.WSU);
            doc.AppendChild(envelope);

            // =====================================================
            // HEADER
            // =====================================================

            XmlElement header = _soapBuilder.CreateHeader(doc);
            envelope.AppendChild(header);

            // =====================================================
            // SECURITY
            // =====================================================

            XmlElement security = doc.CreateElement("o", "Security", SatNamEspaces.WSSE);
            security.SetAttribute("mustUnderstand", SatNamEspaces.SOAP, "1");
            header.AppendChild(security);

            // =====================================================
            // TIMESTAMP
            // =====================================================

            XmlElement timestamp = doc.CreateElement("u", "Timestamp", SatNamEspaces.WSU);
            timestamp.SetAttribute("Id", SatNamEspaces.WSU, "_0");
            security.AppendChild(timestamp);

            XmlElement createdElement = doc.CreateElement("u", "Created", SatNamEspaces.WSU);
            createdElement.InnerText = created.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            timestamp.AppendChild(createdElement);

            XmlElement expiresElement = doc.CreateElement("u", "Expires", SatNamEspaces.WSU);
            expiresElement.InnerText = expires.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            timestamp.AppendChild(expiresElement);

            // =====================================================
            // BINARY SECURITY TOKEN
            // =====================================================

            XmlElement binarySecurityToken = doc.CreateElement("o", "BinarySecurityToken", SatNamEspaces.WSSE);
            binarySecurityToken.SetAttribute("Id", SatNamEspaces.WSU, uuid);
            binarySecurityToken.SetAttribute("ValueType", SatNamEspaces.X509);
            binarySecurityToken.SetAttribute("EncodingType", SatNamEspaces.BASE64);
            binarySecurityToken.InnerText = certBase64;
            security.AppendChild(binarySecurityToken);

            // =====================================================
            // BODY
            // =====================================================

            XmlElement body = _soapBuilder.CreateBody(doc);
            envelope.AppendChild(body);

            XmlElement autentica = doc.CreateElement("Autentica", "http://DescargaMasivaTerceros.gob.mx");
            body.AppendChild(autentica);

            // =====================================================
            // FIRMA DIGITAL
            // =====================================================

            SignedXmlWithId signedXml = new SignedXmlWithId(doc);
            signedXml.SigningKey = cert.GetRSAPrivateKey();
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA1Url;

            // =====================================================
            // REFERENCE
            // =====================================================

            Reference reference = new Reference();
            reference.Uri = "#_0";
            reference.DigestMethod = SignedXml.XmlDsigSHA1Url;
            reference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(reference);

            // =====================================================
            // KEY INFO
            // =====================================================

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoNode keyInfoNode = new KeyInfoNode(CrearSecurityTokenReference(doc, uuid));
            keyInfo.AddClause(keyInfoNode);
            signedXml.KeyInfo = keyInfo;

            // =====================================================
            // GENERAR FIRMA
            // =====================================================

            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();
            security.AppendChild(signature);

            return doc.OuterXml;
        }
        private XmlElement CrearSecurityTokenReference(XmlDocument doc, string uuid)
        {
            XmlElement securityTokenReference = doc.CreateElement("o", "SecurityTokenReference", SatNamEspaces.WSSE);

            XmlElement reference = doc.CreateElement("o", "Reference", SatNamEspaces.WSSE);
            reference.SetAttribute("URI", $"#{uuid}");
            reference.SetAttribute("ValueType", SatNamEspaces.X509);

            securityTokenReference.AppendChild(reference);

            return securityTokenReference;
        }
    }

}
