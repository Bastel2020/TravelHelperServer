using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.DTOs
{
    public class TripInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DestanationId { get; set; }
        public string DestanationName { get; set; }
        public string InviteCode { get; set; }
        public object Users { get; set; }
        public object TripDays { get; set; }
        public TripInfoDTO(Trip tripToParse)
        {
            Id = tripToParse.Id;
            Name = tripToParse.Name;
            Description = tripToParse.Description;
            DestanationId = tripToParse.TripDestanation.Id;
            DestanationName = tripToParse.TripDestanation.Name;
            InviteCode = tripToParse.InviteCode;
            Users = tripToParse.MemberRoles.Select(mr => new
            {
                UserId = mr.UserId,
                Role = mr.Role,
                Username = mr.User.Username
            });
            TripDays = tripToParse.TripDays.Select(td => new
            {
                TripDayId = td.Id,
                Date = td.Date,
                Actions = td.Actions
            });
        }

        public TripInfoDTO()
        {

        }
    }
}
