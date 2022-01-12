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
        public string Chars { get; set; }
        public string Description { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string InviteCode { get; set; }
        public string TripStart { get; set; }
        public string TripEnd{ get; set; }
        public object Users { get; set; }
        public object TripDays { get; set; }
        public TripRole RequiedRoleForInviteCode { get; set; }
        public TripInfoDTO(Trip tripToParse, TripMember role)
        {
            Id = tripToParse.Id;
            Name = tripToParse.Name;
            Description = tripToParse.Description;
            DestinationId = tripToParse.TripDestination.Id;
            DestinationName = tripToParse.TripDestination.Name;
            if (role.Role != TripRole.Viewer)
                RequiedRoleForInviteCode = tripToParse.RequeidRoleToAccessInviteCode;
            if(role.Role <= tripToParse.RequeidRoleToAccessInviteCode)
                InviteCode = tripToParse.InviteCode;
            try
            {
                TripStart = tripToParse.GetStartDate().ToString("d");
                TripEnd = tripToParse.GetEndDate().ToString("d");
            }
            catch { }
            Users = tripToParse.MemberRoles.Select(mr => new
            {
                UserId = mr.UserId,
                Role = mr.Role,
                Username = mr.User.Username,
                CurrentUser = mr.UserId == role.UserId
            });
            TripDays = tripToParse.TripDays
                .OrderBy(td => td.Date)
                .Select(td => new
            {
                TripDayId = td.Id,
                Date = td.Date.ToString("dd.MM"),
                DayOfWeek = td.Date.ToString("ddd"),
                Actions = td.Actions.Select(a => new ActionInfoDTO(a))
            });;

            var c = Name.Split(" ");
            Chars = c.Length == 1 ? c.First()[0].ToString() : String.Concat(c.First()[0], c.Last()[0]);
            Chars = Chars.ToUpper();
        }

        public TripInfoDTO()
        {

        }

        public override bool Equals(object obj)
        {
            if (!(obj is TripInfoDTO))
                return false;

            var castedObj = obj as TripInfoDTO;
            var props = typeof(TripInfoDTO).GetProperties();

            foreach(var property in props)
            {
                var v1 = property.GetValue(obj);
                var v2 = property.GetValue(this);
                if (v1 == null)
                {
                    if (v2 == null)
                        continue;
                    return false;
                }
                if (!v1.Equals(v2))
                    return false;
            }
            return true;
        }
    }
}
