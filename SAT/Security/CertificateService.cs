using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Descargar_CFDIS.Services
{
    public class CertificateService
    {
        public X509Certificate2 CargarFirmaSat(byte[] cerBytes, byte[] keyBytes, string passwordKey)
        {
            var cert = new X509Certificate2(cerBytes);

            AsymmetricKeyParameter key = PrivateKeyFactory.DecryptKey(passwordKey.ToCharArray(),keyBytes);

            #pragma warning disable CA1416
            RSA rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)key);
            #pragma warning restore CA1416

            var finalCert = cert.CopyWithPrivateKey(rsa);

            byte[] pfxBytes = finalCert.Export(X509ContentType.Pfx,passwordKey);

            return new X509Certificate2(pfxBytes,passwordKey,X509KeyStorageFlags.Exportable |X509KeyStorageFlags.MachineKeySet);
        }
        public X509Certificate2 CargarPfxSat(byte[] pfxBytes,string passwordKey)
        {
            return new X509Certificate2(pfxBytes,passwordKey,X509KeyStorageFlags.Exportable |X509KeyStorageFlags.MachineKeySet);
        }
    }
}