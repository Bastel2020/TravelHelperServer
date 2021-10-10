using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Repositories
{
    public class DefaultAuthRepository : IAuthRepository
    {
        public bool AuthUser(LoginDataDTO data)
        {
            throw new NotImplementedException();
        }

        public bool LogOutUser(NoDataActionDTO data)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(RegisterUserDTO data)
        {
            throw new NotImplementedException();
        }
    }
}
