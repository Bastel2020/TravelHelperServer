using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Enums;

namespace TravelHelperBackend.Database.Models
{
    public class Trip
    {
        public long Id { get; set }
        public User Owner { get; set; }
        public List<User> Editors { get; set; }
        public List<User> Viewers { get; set; }
        public City TripDestanation { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Массив дней путешествия: в нём хранится информация о событиях в каждом из дней.
        /// </summary>
        public List<TripDay> TripDays { get; set; }

        public string GetCityName()
        {
            return TripDestanation.Name;
        }

        public DateTime GetStartDate()
        {

        }

        public DateTime GetEndDate()
        {

        }
    }
}
