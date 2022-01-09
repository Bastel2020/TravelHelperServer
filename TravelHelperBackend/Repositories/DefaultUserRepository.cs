using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
                .ThenInclude(ut => ut.TripDestination)
                .Include(u => u.UserTrips)
                .ThenInclude(t => t.Members)
                .ThenInclude(m => m.Avatar)
                .Include(u => u.UserTrips)
                .ThenInclude(t => t.TripDays)
                .ThenInclude(td => td.Actions)
                .Include(u => u.UserTrips)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            return new UserInfoDTO(user);
        }

        //public async Task<TripInfoDTO[]> GetUserTrips(string email)
        //{
        //    var user = await _db.Users
        //        .FirstOrDefaultAsync(u => u.Email == email);
        //    if (user == null)
        //        return null;

        //    var trips = _db.Trips
        //        .Where(t => t.Members.Contains(user))
        //        .Include(t => t.Members)
        //        .Include(t => t.MemberRoles)
        //        .Include(t => t.TripDays)
        //        .ThenInclude(td => td.Actions)
        //        .Include(t => t.TripDestination);

        //    if (user == null)
        //        return null;

        //    return new UserInfoDTO(user);
        //}

        public async Task<bool> UploadAvatar(IFormFile file, string email)
        {
            var fileExtensionParts = file.FileName.Split('.');
            string fileExtension;
            if (fileExtensionParts.Length < 2)
                return false;
            else
                fileExtension = fileExtensionParts[fileExtensionParts.Length - 1].ToLower();
            if (fileExtension != "jpg" && fileExtension != "jpeg" && fileExtension != "png")
                return false;

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || file.Length > 1000000)
                return false;

            string path = $"/userFiles/Users/{user.Id}/avatar.{fileExtension}";
            using (var fileStream = new FileStream(Environment.CurrentDirectory + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            FileModel avatarModel = new FileModel { Name = file.FileName, Path = path };
            user.Avatar = avatarModel;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAvatar(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            try
            {
                File.Delete(user.Avatar.Path);
                _db.Files.Remove(user.Avatar);

                return true;
            }
            catch { return false; }
        }

        public async Task<byte[]> GetAvatar(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return null;
            if (user.Avatar == null)
                return null;
            return await File.ReadAllBytesAsync(user.Avatar.Path);
        }

        public async Task<bool> AddOrRemoveFromFavorites(string email, int placeId)
        {
            var user = await _db.Users
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            var place = user.Favorites.FirstOrDefault(f => f.Id == placeId);

            if (place != null)
            {
                user.Favorites.Remove(place);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                place = await _db.Places.FirstOrDefaultAsync(p => p.Id == placeId);

                if (place == null)
                    return false;

                user.Favorites.Add(place);
                await _db.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> IsInFavorites(string email, int placeId)
        {
            var user = await _db.Users
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return false;

            return (user.Favorites.FirstOrDefault(p => p.Id == placeId) != null);
        }

        public async Task<PlaceInfoDTO[]> GetAllFavorites(string email)
        {
            var user = await _db.Users
                .Include(u => u.Favorites)
                .ThenInclude(p => p.MainPhoto)
                .Include(u => u.Favorites)
                .ThenInclude(p => p.Photos)
                .Include(u => u.Favorites)
                .ThenInclude(p => p.City)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            return user.Favorites
                .Select(p => new PlaceInfoDTO(p))
                .ToArray();
        }
    }
}
