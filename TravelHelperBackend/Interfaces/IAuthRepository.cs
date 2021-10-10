using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface IAuthRepository
    {
        public bool RegisterUser(RegisterUserDTO data);
        public bool AuthUser(LoginDataDTO data);
        public bool LogOutUser(NoDataActionDTO data);

    }
}