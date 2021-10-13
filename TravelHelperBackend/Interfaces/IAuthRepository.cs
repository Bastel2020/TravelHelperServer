using System.Security.Claims;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface IAuthRepository
    {
        public Task<bool> RegisterUser(RegisterUserDTO data);
        public Task<string> AuthUser(LoginDataDTO data);
        public Task<bool> LogOutUser(NoDataActionDTO data);

    }
}