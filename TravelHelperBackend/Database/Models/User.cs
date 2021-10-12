using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using TravelHelperBackend.Helpers;

namespace TravelHelperBackend.Database.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        private string _password;
        public string Password { get => _password;  set
            {
                _password = PasswordHasher.GetSHA512HashedPassword(value, Email);
            }
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
