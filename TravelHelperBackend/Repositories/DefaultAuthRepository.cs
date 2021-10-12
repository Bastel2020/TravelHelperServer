using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;
using TravelHelperBackend.Database;
using TravelHelperBackend.Helpers;
using System.Security.Claims;

namespace TravelHelperBackend.Repositories
{
    public class DefaultAuthRepository : IAuthRepository
    {
        private DefaultDbContext _db;
        public DefaultAuthRepository(DefaultDbContext db)
        {
            _db = db;
        }
        public async Task<ClaimsIdentity> AuthUser(LoginDataDTO data)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == data.Email);
            if (user == null)
                return null;
            if (user.Password == PasswordHasher.GetSHA512HashedPassword(data.Password, data.Email))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, data.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public async Task<bool> LogOutUser(NoDataActionDTO data)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(RegisterUserDTO data)
        {
            try
            {
                if (_db.Users.FirstOrDefault(x => x.Email == data.Email) != null || _db.Users.FirstOrDefault(x => x.Username == data.Username) != null)
                    return false;
                _db.Users.Add(new Database.Models.User { Email = data.Email, Password = data.Password, Username = data.Username, FirstName = data.FirstName, SecondName = data.SecondName });
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}
