using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> ChangePassword(ChangePasswordDTO data);
        public Task<bool> EditProfile(EditUserProfileDTO data);
    }
}
