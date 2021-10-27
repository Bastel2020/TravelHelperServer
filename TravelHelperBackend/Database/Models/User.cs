using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using TravelHelperBackend.Helpers;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace TravelHelperBackend.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        private string _password;
        [JsonIgnore]
        public string Password { get => _password;  set
            {
                _password = PasswordHasher.GetSHA512HashedPassword(value, Email);
            }
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<Trip> UserTrips { get; set; }
        [JsonIgnore]
        public List<TripMember> TripRoles { get; set; }
        [JsonIgnore]
        public List<PollVariants> VotesInPolls { get; set; }
    }
}
