using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.Enums;

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
        public object Polls { get; set; }
        public TripRole RequiedRoleForInviteCode { get; set; }
        public TripInfoDTO(Trip tripToParse, TripRole role)
        {
            Id = tripToParse.Id;
            Name = tripToParse.Name;
            Description = tripToParse.Description;
            DestinationId = tripToParse.TripDestination.Id;
            DestinationName = tripToParse.TripDestination.Name;
            if (role != TripRole.Viewer)
                RequiedRoleForInviteCode = tripToParse.RequeidRoleToAccessInviteCode;
            if(role <= tripToParse.RequeidRoleToAccessInviteCode)
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
            Polls = tripToParse.Polls;
        }

        public TripInfoDTO()
        {

        }
    }
}
