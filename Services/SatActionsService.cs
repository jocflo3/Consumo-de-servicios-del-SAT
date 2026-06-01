using CFDI_DESCARGA.SAT.Builders;
using Descargar_CFDIS.DTOS;
using Descargar_CFDIS.Excepciones;
using Descargar_CFDIS.Helpers;
using Descargar_CFDIS.Interfaces.Repository;
using Descargar_CFDIS.Interfaces.Service;
using Descargar_CFDIS.SAT.Builders;
using Descargar_CFDIS.SAT.Clients;
using Descargar_CFDIS.SAT.Constants;
using Descargar_CFDIS.SAT.Parsers;
using Descargar_CFDIS.SAT.Security;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Descargar_CFDIS.Services
{
    public class SatActionsService: ISatActionsService
    {
        private readonly CertificateService _certificate;
        private readonly SatAuthXmlBuilder _SatAuthXml;
        private readonly SatSolicitudFolioXmlBuilder _SatSolUUIDXml;
        private readonly SatXmlVerificarSolicitud _SatSolVeriXml;
        private readonly SatSoapClient _soapClient;
        private readonly AuthResponseParser _responseParser;
        private readonly ISatActionsRepository _actions;
        private readonly PasswordProtector _pass;
        public SatActionsService(CertificateService certificate, SatAuthXmlBuilder SatAuthXml, SatSoapClient soapClient, 
            AuthResponseParser responseParser, ISatActionsRepository actions, PasswordProtector pass, SatSolicitudFolioXmlBuilder SatSolUUIDXml,
            SatXmlVerificarSolicitud SatSolVeriXml) 
        {
            _certificate = certificate;
            _SatAuthXml = SatAuthXml;
            _soapClient = soapClient;
            _responseParser = responseParser;
            _actions = actions;
            _pass = pass;
            _SatSolUUIDXml = SatSolUUIDXml;
            _SatSolVeriXml = SatSolVeriXml;
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
            };
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
        public async Task<string> GeneraSolicitudUUID(int id, string UUID, string token)
        {
            var user = await _actions.ObtenerPfxPass(id);
            if (user == null)
            {
                throw new EmptyException("No se encontro información");
            }
            X509Certificate2 cert = _certificate.CargarPfxSat(user.PfxFile, _pass.Unprotect(user.PasswordKeyEncrypted));
            string xml = _SatSolUUIDXml.GenerarXmlSolicitudFolio(cert,UUID);

            string response = await _soapClient.PostAsync(xml, SatEndpoints.SolUUID, SatSoapActions.SolDesUUID, token);

            string resp = _responseParser.ExtractIdSolicitud(response);

           // return token;
            return "";
        }
        public async Task<string> VerificaSolicitud(int id, string IdSolicitud, string token)
        {
            var user = await _actions.ObtenerPfxPass(id);
            if (user == null)
            {
                throw new EmptyException("No se encontro información");
            }
            X509Certificate2 cert = _certificate.CargarPfxSat(user.PfxFile, _pass.Unprotect(user.PasswordKeyEncrypted));
            string xml = _SatSolVeriXml.GenerarXmlVerificarSolicitud(cert, IdSolicitud,user.RFC);

            string response = await _soapClient.PostAsync(xml, SatEndpoints.SolVer, SatSoapActions.Verifica, token);

            string resp = _responseParser.ExtractIdSolicitud(response);

            // return token;
            return resp;
        }
        public async Task<string> DescargarSolicitud(int id, string IdPaquete, string token)
        {
            var user = await _actions.ObtenerPfxPass(id);
            if (user == null)
            {
                throw new EmptyException("No se encontro información");
            }
            X509Certificate2 cert = _certificate.CargarPfxSat(user.PfxFile, _pass.Unprotect(user.PasswordKeyEncrypted));
            string xml = _SatSolVeriXml.GenerarXmlVerificarSolicitud(cert, IdPaquete, user.RFC);

            string response = await _soapClient.PostAsync(xml, SatEndpoints.SolVer, SatSoapActions.Verifica, token);

            string resp = _responseParser.ExtractIdSolicitud(response);

            // return token;
            return resp;
        }
    }
}
