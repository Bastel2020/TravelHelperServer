using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> ChangePassword(ChangePasswordDTO data, string email);
        public Task<UserInfoDTO> EditProfile(EditUserProfileDTO data, string email);
        public Task<UserInfoDTO> GetProfile(string email);
        public Task<bool> UploadAvatar(IFormFile file, string email);
        public Task<bool> DeleteAvatar(string email);
        public Task<byte[]> GetAvatar(int userId);
    }
}
