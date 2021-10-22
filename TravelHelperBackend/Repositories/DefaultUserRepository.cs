using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Helpers;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Repositories
{
    public class DefaultUserRepository : IUserRepository
    {
        private DefaultDbContext _db;
        public DefaultUserRepository(DefaultDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO data, string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;
            if (user.Password != PasswordHasher.GetSHA512HashedPassword(data.OldPassword, user.Email))
                return false;
            if (data.NewPassword != data.NewPasswordRepeat)
                return false;

            user.Password = PasswordHasher.GetSHA512HashedPassword(data.NewPassword, user.Email);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<UserInfoDTO> EditProfile(EditUserProfileDTO data, string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            if (data.FirstName != null)
                user.FirstName = data.FirstName;
            if (data.SecondName != null)
                user.SecondName = data.SecondName;
            if (data.Username != null && await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == data.Username.ToLower()) == null)
                user.Username = data.Username;

            await _db.SaveChangesAsync();
            return await GetProfile(email);
        }

        public async Task<UserInfoDTO> GetProfile(string email)
        {
            var user = await _db.Users
                .Include(u => u.UserTrips)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            return new UserInfoDTO(user);
        }
    }
}
