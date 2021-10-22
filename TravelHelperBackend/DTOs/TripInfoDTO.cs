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
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string InviteCode { get; set; }
        public DateTime TripStart { get; set; }
        public DateTime TripEnd{ get; set; }
        public object Users { get; set; }
        public object TripDays { get; set; }
        public TripInfoDTO(Trip tripToParse)
        {
            Id = tripToParse.Id;
            Name = tripToParse.Name;
            Description = tripToParse.Description;
            DestinationId = tripToParse.TripDestination.Id;
            DestinationName = tripToParse.TripDestination.Name;
            InviteCode = tripToParse.InviteCode;
            try
            {
                TripStart = tripToParse.GetStartDate();
                TripEnd = tripToParse.GetEndDate();
            }
            catch { }
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
