using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<TripShortInfo> UserTrips { get; set; }
        public UserInfoDTO() { }
        public UserInfoDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            FirstName = user.FirstName;
            SecondName = user.SecondName;

            UserTrips = user.UserTrips
                .Select(ut => new TripShortInfo() { Id = ut.Id, Name = ut.Name })
                .ToList();
        }
    }

    public class TripShortInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
