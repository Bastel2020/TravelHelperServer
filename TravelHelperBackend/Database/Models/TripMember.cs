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

        public TripRolesEnum Role { get; set; }
    }
}
