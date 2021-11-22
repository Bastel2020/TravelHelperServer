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

        public async Task<Trip> GetTrip(int id)
        {
            return await _db.Trips
                .Include(t => t.Members)
                .Include(t => t.MemberRoles)
                .Include(t => t.TripDays)
                .ThenInclude(td => td.Actions)
                .Include(t => t.TripDestination)
                .Include(t => t.Polls)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TripInfoDTO> CreateTrip(CreateTripDTO data, string email)
        {
            var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
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

            var newTrip = new Trip() { Name = data.Name, Description = data.Description, Members = new List<User>() { currentUser }, TripDays = TripDays, TripDestination = destanationCity};
            _db.Trips.Add(newTrip);

            await _db.SaveChangesAsync();

            return await GetTripInfo(newTrip.Id, email);
        }

        public async Task<TripInfoDTO> CreateTripWithoutDates(CreateTripWithoutDatesDTO data, string email)
        {
            var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (currentUser == null)
                return null;

            var destanationCity = _db.Cities.Where(c => c.Id == data.TripDestanation).FirstOrDefault();
            if (destanationCity == null)
                return null;

            var newTrip = new Trip() { Name = data.Name, Description = data.Description, Members = new List<User>() { currentUser }, TripDestination = destanationCity };
            _db.Trips.Add(newTrip);

            await _db.SaveChangesAsync();

            return await GetTripInfo(newTrip.Id, email);
        }

        public async Task<TripInfoDTO> EditTripInfo(EditTripInfoDTO data, string email)
        {
            var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (currentUser == null)
                return null;

            var trip = await GetTrip(data.Id);
            if (trip == null || trip.Members.FirstOrDefault(u => u.Email == email) == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == currentUser.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            if (data.Destination != -1)
            {
                var newCity = await _db.Cities.FirstOrDefaultAsync(c => c.Id == data.Id);
                if (newCity == null)
                    return null;
                throw new NotImplementedException("Изменение пункта назначения пока недоступно"); //Необходимо реализовать логику удаления всех Actions, связанных со старым городом, из поездки.
                //trip.TripDestination = newCity;
            }

            if (data.Name != null)
                trip.Name = data.Name;
            if (data.Description != null)
                trip.Description = data.Description;

            await _db.SaveChangesAsync();

            return await GetTripInfo(trip.Id, email);
        }

        public async Task<TripInfoDTO> GetTripInfo(int tripId, string email)
        {
            var trip = await GetTrip(tripId);
            var tripMember = trip.Members.FirstOrDefault(u => u.Email == email);
            if (trip == null || tripMember == null)
                return null;
            var role = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == tripMember.Id).Role;
            return new TripInfoDTO(trip, role);
        }

        public async Task<TripInfoDTO> GenerateInviteCode(int tripId, string email)
        {
            var trip = await GetTrip(tripId);
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

        public async Task<TripInfoDTO> JoinByInviteCode(string invite, string email)
        {
            int id;
            try { id = int.Parse(invite.Split(':')[0]); }
            catch { return null; }

            var trip = await GetTrip(id);
            if (trip == null || trip.InviteCode != invite)
                return null;

            if (!await AddUserToTripWithoutСheck(trip, email))
                return null;

            return await GetTripInfo(trip.Id, email);
        }

        public async Task<bool> AddUserToTrip(string emailToInvite, int tripId, string email)
        {
            var trip = await GetTrip(tripId);


            var inviter = trip.Members.FirstOrDefault(u => u.Email == email);
            if (inviter == null)
                return false;

            return await AddUserToTripWithoutСheck(trip, emailToInvite);
        }

        public async Task<bool> DeleteTrip(int tripId, string email)
        {
            var currentUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (currentUser == null)
                return false;

            var trip = await GetTrip(tripId);
            if (trip == null)
                return false;

            var deleterRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == currentUser.Id);
            if (deleterRole == null || deleterRole.Role != Enums.TripRole.Owner)
                return false;

            _db.Trips.Remove(trip);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddUserToTripWithoutСheck(Trip tripToAdd, string emailToAdd)
        {
            var userToAdd = await _db.Users.FirstOrDefaultAsync(u => u.Email == emailToAdd);
            if (userToAdd == null || tripToAdd.Members.Contains(userToAdd))
                return false;

            tripToAdd.Members.Add(userToAdd);
            await _db.SaveChangesAsync();

            tripToAdd.MemberRoles
                .First(mr => mr.UserId == userToAdd.Id)
                .Role = Enums.TripRole.Viewer;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TripInfoDTO> VoteInPoll(int pollId, int selectedOption, string email)
        {
            var poll = await _db.Polls
                .Include(p => p.Parent)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == pollId);
            if (poll == null)
                return null;
            var trip = await GetTrip(poll.Parent.Id);
            var user = trip.Members.FirstOrDefault(m => m.Email == email);
            if (user != null)
                return null;
            poll.Variants[selectedOption].Votes.Add(user);

            await _db.SaveChangesAsync();

            return await GetTripInfo(poll.Parent.Id, email);
        }
    }
}
