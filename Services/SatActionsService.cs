using Descargar_CFDIS.Excepciones;
using Descargar_CFDIS.Interfaces.Repository;
using Descargar_CFDIS.SAT.Security;
using Descargar_CFDIS.SAT.Builders;
using Descargar_CFDIS.SAT.Clients;
using Descargar_CFDIS.SAT.Constants;
using Descargar_CFDIS.SAT.Parsers;
using System.Security.Cryptography.X509Certificates;
using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Interfaces.Service;
using Descargar_CFDIS.Helpers;

namespace Descargar_CFDIS.Services
{
    public class SatActionsService: ISatActionsService
    {
        private readonly CertificateService _certificate;
        private readonly SatAuthXmlBuilder _SatAuthXml;
        private readonly SatSoapClient _soapClient;
        private readonly AuthResponseParser _responseParser;
        private readonly ISatActionsRepository _actions;
        private readonly PasswordProtector _pass;
        public SatActionsService(CertificateService certificate, SatAuthXmlBuilder SatAuthXml, SatSoapClient soapClient, 
            AuthResponseParser responseParser, ISatActionsRepository actions, PasswordProtector pass) 
        {
            _certificate = certificate;
            _SatAuthXml = SatAuthXml;
            _soapClient = soapClient;
            _responseParser = responseParser;
            _actions = actions;
            _pass = pass;
        }
        public async Task GeneraYRegistrarPfx(RegisterCertificateDTO user, int id)
        {
            byte[] cer = await ObtenerBytesAsync(user.CerFile);
            byte[] key = await ObtenerBytesAsync(user.KeyFile);

            X509Certificate2 cert = _certificate.CargarFirmaSat(cer, key,user.Password);

            if (cert == null)
            {
                throw new EmptyException("No se genero correctamente el archivo pfx");
            }
            byte[] pfxBytes = cert.Export(X509ContentType.Pfx, user.Password);
            var val = await _actions.GeneraYRegistrarPfx(pfxBytes, _pass.Protect(user.Password), id);

            if (val == 0)
            {
                throw new NotFoundException("Usuario", id);
            }
            ;
            //X509Certificate2 cert = _certificate.CargarPfxSat(user.PfxFile, _pass.Unprotect(user.PasswordKeyEncrypted));
            //string xml = _SatAuthXml.GenerarXmlAutenticacion(cert);

            //string response = await _soapClient.PostAsync(xml, SatEndpoints.Auth, SatSoapActions.Autentica);

            //string token = _responseParser.ExtractToken(response);

        }
        public async Task<string> AuthenticateAsync(int id)
        {
            var user = await _actions.ObtenerPfxPass(id);
            if (user == null)
            {
                throw new EmptyException("No se encontro información");
            }
            X509Certificate2 cert = _certificate.CargarPfxSat(user.PfxFile, _pass.Unprotect(user.PasswordKeyEncrypted));
            string xml = _SatAuthXml.GenerarXmlAutenticacion(cert);

            string response = await _soapClient.PostAsync(xml, SatEndpoints.Auth, SatSoapActions.Autentica);

            string token = _responseParser.ExtractToken(response);

            return token;
        }
        public async Task<byte[]> ObtenerBytesAsync(IFormFile file)
        {
            using MemoryStream ms = new();

            await file.CopyToAsync(ms);

            return ms.ToArray();
        }
    }
}
