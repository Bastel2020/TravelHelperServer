﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;
using TravelHelperBackend.Database;
using TravelHelperBackend.Helpers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TravelHelperBackend.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace TravelHelperBackend.Repositories
{
    public class DefaultAuthRepository : IAuthRepository
    {
        private DefaultDbContext _db;
        public DefaultAuthRepository(DefaultDbContext db)
        {
            _db = db;
        }
        public async Task<string> AuthUser(LoginDataDTO data)
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

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = claimsIdentity.Name,
                    lifetime = AuthOptions.LIFETIME
                };

                return Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);
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
