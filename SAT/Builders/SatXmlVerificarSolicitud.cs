using Descargar_CFDIS.SAT.Constants;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Descargar_CFDIS.SAT.Builders
{
    public class SatXmlVerificarSolicitud
    {
        private readonly SatSoapBuilder _soapBuilder;

        public SatXmlVerificarSolicitud()
        {
            _soapBuilder = new SatSoapBuilder();
        }
        public string GenerarXmlVerificarSolicitud(X509Certificate2 cert, string idSolicitud, string rfcSolicitante)
        {

            XmlDocument doc = _soapBuilder.CreateDoc();

            // =====================================================
            // ENVELOPE
            // =====================================================

            XmlElement envelope = _soapBuilder.CreateEnvelope(doc);
            envelope.SetAttribute("xmlns:des", SatNamEspaces.DescargaMasiva);
            envelope.SetAttribute("xmlns:xd",  SatNamEspaces.XmlDsig);
            doc.AppendChild(envelope);

            // =====================================================
            // HEADER
            // =====================================================

            XmlElement header = _soapBuilder.CreateHeader(doc);
            envelope.AppendChild(header);

            // =====================================================
            // BODY
            // =====================================================

            XmlElement body = _soapBuilder.CreateBody(doc);
            envelope.AppendChild(body);

            // =====================================================
            // VerificaSolicitudDescarga
            // =====================================================

            XmlElement verificaSolicitud = doc.CreateElement("des","VerificaSolicitudDescarga",SatNamEspaces.DescargaMasiva);
            body.AppendChild(verificaSolicitud);

            // =====================================================
            // solicitud
            // =====================================================

            XmlElement solicitud = doc.CreateElement("des","solicitud",SatNamEspaces.DescargaMasiva);
            solicitud.SetAttribute("IdSolicitud",idSolicitud);
            solicitud.SetAttribute("RfcSolicitante",  rfcSolicitante);
            verificaSolicitud.AppendChild(solicitud);

            // =====================================================
            // FIRMA DIGITAL
            // =====================================================

            SignedXml signedXml = new SignedXml(solicitud);
            signedXml.SigningKey = cert.GetRSAPrivateKey();
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA1Url;

            // =====================================================
            // REFERENCE
            // =====================================================

            Reference reference = new();
            reference.Uri = "";
            reference.DigestMethod = SignedXml.XmlDsigSHA1Url;
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(reference);

            // =====================================================
            // KEY INFO
            // =====================================================

            KeyInfo keyInfo = new();
            KeyInfoX509Data x509Data = new KeyInfoX509Data(cert);
            x509Data.AddIssuerSerial(cert.Issuer,cert.SerialNumber);
            keyInfo.AddClause(x509Data);
            signedXml.KeyInfo = keyInfo;

            // =====================================================
            // GENERAR FIRMA
            // =====================================================

            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();
            solicitud.AppendChild(doc.ImportNode(signature, true));

            return doc.OuterXml;
        }
    }
}
