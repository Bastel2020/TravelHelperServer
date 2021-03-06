using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.Database.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public bool OwnerLeaves { get; set; }
        public List<User> Members { get; set; }
        public List<TripMember> MemberRoles { get; set; }
        public City TripDestination { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Код, который нужно ввести пользователю, чтобы присоединиться к поездке. Если == null, то присоединиться к поездке по коду нельзя.
        /// </summary>
        public string InviteCode { get; set; }
        public TripRole RequeidRoleToAccessInviteCode { get; set; }
        /// <summary>
        /// Массив дней путешествия: в нём хранится информация о событиях в каждом из дней.
        /// </summary>
        public List<TripDay> TripDays { get; set; }

        public string GetCityName()
        {
            return TripDestination.Name;
        }

        public DateTime GetStartDate()
        {
            if (TripDays != null && TripDays.Count > 0)
                return TripDays.Select(td => td.Date)
                    .OrderBy(d => d)
                    .First();
            else
                return new DateTime();
        }

        public DateTime GetEndDate()
        {
            if (TripDays != null && TripDays.Count > 0)
                return TripDays.Select(td => td.Date)
                    .OrderBy(d => d)
                    .Last();
            else
                return new DateTime();
        }

        public override string ToString()
        {
            return $"Id:{Id}, {Name}";
        }
    }
}
