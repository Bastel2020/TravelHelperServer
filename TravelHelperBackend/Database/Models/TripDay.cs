using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelHelperBackend.Database.Models
{
    public class TripDay
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public Trip Parent { get; set; }
        public List<TripAction> Actions { get; set; }

        /// <summary>
        /// Создать экземляр TripDay с заполненным временем. Массив Actions - new List<TripAction>(). Id не заполняется, отдаётся на откуп БД.
        /// </summary>
        /// <param name="dateTime">Дата для TripDay.</param>
        public TripDay(DateTime dateTime)
        {
            Date = dateTime;
            Actions = new List<TripAction>();
        }

        /// <summary>
        /// Создать экземляр TripDay без параметров.
        /// </summary>
        public TripDay()
        {
        }
    }
}
