using Microsoft.AspNetCore.DataProtection;

namespace Descargar_CFDIS.SAT.Security
{
    public class PasswordProtector
    {
        private readonly IDataProtector _protector;

        public PasswordProtector(
            IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("SAT-PASSWORDS");
        }

        public string Protect(string password)
        {
            return _protector.Protect(password);
        }

        public string Unprotect(string protectedPassword)
        {
            return _protector.Unprotect(protectedPassword);
        }
    }
}
