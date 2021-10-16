using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelHelperBackend.Database;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Repositories
{
    public class DefaultTripRepository : ITripRepository
    {
        private DefaultDbContext _db;
        public DefaultTripRepository(DefaultDbContext db)
        {
            _db = db;
        }

        public Task<bool> AddTripDay(AddTripDayDTO data, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<TripInfoDTO> CreateTrip(CreateTripDTO data, string email)
        {
            var currentUser = _db.Users.FirstOrDefault(u => u.Email == email);
            if (currentUser == null || data.Name == null)
                return null;

            var TripDays = new List<TripDay>();
            for (var i = data.StartDate; i < data.EndDate; i = i.AddDays(1))
            {
                TripDays.Add(new TripDay(i));
            }

            var destanationCity = _db.Cities.Where(c => c.Id == data.TripDestanation).FirstOrDefault();
            if (destanationCity == null)
                return null;

            var newTrip = new Trip() { Name = data.Name, Description = data.Description, Members = new List<User>() { currentUser }, TripDays = TripDays, TripDestanation = destanationCity};
            _db.Trips.Add(newTrip);

            await _db.SaveChangesAsync();

            return await GetTripInfo(newTrip.Id, email);
        }

        public async Task<TripInfoDTO> CreateTripWithoutDates(CreateTripWithoutDatesDTO data, string email)
        {
            var currentUser = _db.Users.FirstOrDefault(u => u.Email == email);
            if (currentUser == null)
                return null;

            var destanationCity = _db.Cities.Where(c => c.Id == data.TripDestanation).FirstOrDefault();
            if (destanationCity == null)
                return null;

            var newTrip = new Trip() { Name = data.Name, Description = data.Description, Members = new List<User>() { currentUser }, TripDestanation = destanationCity };
            _db.Trips.Add(newTrip);

            await _db.SaveChangesAsync();

            return await GetTripInfo(newTrip.Id, email);
        }

        public Task<bool> EditTripInfo(EditTripInfoDTO data, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<TripInfoDTO> GetTripInfo(int tripId, string email)
        {
            var trip = _db.Trips
                .Include(t => t.Members)
                .Include(t => t.MemberRoles)
                .Include(t => t.TripDays)
                .ThenInclude(td => td.Actions)
                .Include(t => t.TripDestanation)
                .FirstOrDefault(t => t.Id == tripId);
            if (trip == null || trip.Members.FirstOrDefault(u => u.Email == email) == null)
                return null;
            return new TripInfoDTO(trip);

        }

        public async Task<TripInfoDTO> GenerateInviteCode(int tripId, string email)
        {
            var trip = _db.Trips
                .Include(t => t.Members)
                .FirstOrDefault(t => t.Id == tripId);
            if (trip == null || trip.Members.FirstOrDefault(u => u.Email == email) == null)
                return null;

            var rnd = new Random();
            var rndChars = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                var rndNumber = rnd.Next(0, 62);
                rndChars[i] = (byte)(rndNumber < 10 ? 48 + rndNumber : rndNumber < 36 ? 55 + rndNumber : 61 + rndNumber); //Генерируем случайную строку из 8 символов согласно ASCII таблице.
            }
            trip.InviteCode = trip.Id + ":" + Encoding.ASCII.GetString(rndChars); //Генерируем код доступа в виде *Id поедки*:*Случайный код*

            await _db.SaveChangesAsync();

            return await GetTripInfo(tripId, email);
        }
    }
}
