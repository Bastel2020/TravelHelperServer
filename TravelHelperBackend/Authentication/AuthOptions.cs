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
        static readonly string KEY = "DWA!saf348j1cv0";   // ключ для шифрации
        public static readonly int LIFETIME = 60; // время жизни токена - 1 час
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}
