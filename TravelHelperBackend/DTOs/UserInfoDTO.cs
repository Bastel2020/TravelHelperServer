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
                .Select(ut => new TripShortInfo(ut, user))
                .ToList();
        }
    }

    public class TripShortInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TripStart { get; set; }
        public string TripEnd { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public int ActionsCount { get; set; }
        public TripMembersShortInfo[] FirstTwoUsers { get; set; }
        public int AdditionalUserCount { get; set; }

        public TripShortInfo(Trip data, User requster)
        {
            Id = data.Id;
            Name = data.Name;
            TripStart = data.TripDays.Min(td => td.Date).ToString("d");
            TripEnd = data.TripDays.Max(td => td.Date).ToString("d");
            DestinationId = data.TripDestination.Id;
            DestinationName = data.TripDestination.Name;
            ActionsCount = data.TripDays
                .Select(td => td.Actions.Count)
                .Sum();

            if (data.Members.Count > 2)
                FirstTwoUsers = data.Members
                    .Take(2)
                    .Select(m => new TripMembersShortInfo(m))
                    .ToArray();

            if (data.Members.Count == 2)
                FirstTwoUsers = data.Members
                    .Select(m => new TripMembersShortInfo(m))
                    .ToArray();

            AdditionalUserCount = data.Members.Count > 2 ? data.Members.Count - 3 : 0;
        }
    }

    public class TripMembersShortInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AvatarPath { get; set; }

        public TripMembersShortInfo(User userToParse)
        {
            Id = userToParse.Id;
            Username = userToParse.Username;
            if (userToParse.Avatar != null)
                AvatarPath = userToParse.Avatar.Path;
        }
    }
}
