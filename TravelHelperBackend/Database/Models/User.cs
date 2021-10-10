using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace TravelHelperBackend.Database.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        private byte[] _password;
        public string Password { get => Encoding.UTF8.GetString(_password); set
            {
                using (SHA512 shaM = new SHA512Managed())
                {
                    _password = shaM.ComputeHash(Encoding.UTF8.GetBytes(value + Email));
                }

            }
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
