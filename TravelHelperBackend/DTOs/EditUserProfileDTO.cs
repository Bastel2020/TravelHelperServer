using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelHelperBackend.DTOs
{
    public class EditUserProfileDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
