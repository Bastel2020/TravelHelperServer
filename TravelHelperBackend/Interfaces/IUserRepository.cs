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
    }
}
