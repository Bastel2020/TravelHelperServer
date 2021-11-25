using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelHelperBackend.Authentication
{
    public class AuthOptions
    {
        public static readonly string ISSUER = "TravelHelperAuthServer"; // издатель токена
        public static readonly string AUDIENCE = "TravelHelperClient"; // потребитель токена
        static readonly string KEY = "DWA!saf348j1cv0SecretKey";   // ключ для шифрации
        public static readonly int LIFETIME = 43800; // время жизни токена - 1 месяц
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
