using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.Database.Models
{
    public class TripMember
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public TripRole Role { get; set; }

        public TripMember() { }

        public TripMember(User userToAdd, Trip tripToAdd, TripRole role)
        {
            User = userToAdd;
            Trip = tripToAdd;
            UserId = userToAdd.Id;
            TripId = tripToAdd.Id;
            Role = role;
        }
    }
}
