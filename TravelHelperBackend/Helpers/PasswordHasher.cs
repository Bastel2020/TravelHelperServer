using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TravelHelperBackend.Helpers
{
    public class PasswordHasher
    {
        private static SecuritySettings _secOptions = new SecuritySettings(); //Всегда null!
        public PasswordHasher(IOptions<SecuritySettings> secOpts)
        {
            _secOptions = secOpts.Value;
        }
        public static string GetSHA512HashedPassword(string password, string email)
        {
            SHA512 shaM = new SHA512Managed();
            var _password = shaM.ComputeHash(Encoding.UTF8.GetBytes(password + email + _secOptions.PasswordSalt));
            return Encoding.UTF8.GetString(_password);
        }
    }
}
