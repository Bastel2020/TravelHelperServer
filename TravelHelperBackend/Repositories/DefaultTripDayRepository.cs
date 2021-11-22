using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Repositories
{
    public class DefaultTripDayRepository : ITripDayRepository
    {
        private DefaultDbContext _db;
        private ITripRepository _trRep;
        public DefaultTripDayRepository(DefaultDbContext db, ITripRepository tripRepository)
        {
            _db = db;
            _trRep = tripRepository;
        }
        public async Task<TripInfoDTO> AddAction(AddActionDTO data, string email)
        {
            if (data.TripDayId == 0 || data.Name == null || data.TimeOfAction == new TimeSpan(-1, 0, 0))
                return null;

            var tripDay = await _db.TripDays
                    .Include(td => td.Parent)
                    .Include(td => td.Actions)
                    .FirstOrDefaultAsync(td => td.Id == data.TripDayId);

            if (tripDay == null)
                return null;

            var trip = await _trRep.GetTrip(tripDay.Parent.Id);

            var editor = trip.Members.FirstOrDefault(m => m.Email == email);
            if (editor == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == editor.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            tripDay.Actions.Add(new TripAction(data));
            await _db.SaveChangesAsync();

            return await _trRep.GetTripInfo(trip.Id, email);
        }

        public async Task<TripInfoDTO> AddTripDay(AddTripDayDTO data, string email)
        {
            var trip = await _trRep.GetTrip(data.Id);

            var currentUser = trip.Members.FirstOrDefault(u => u.Email == email);
            if (trip == null || currentUser == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == currentUser.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            var tripDayWithSameDate = trip.TripDays.FirstOrDefault(td => td.Date == data.TimeToAdd);
            if (tripDayWithSameDate != null)
                return null;

            trip.TripDays.Add(new TripDay(data.TimeToAdd));
            await _db.SaveChangesAsync();

            return await _trRep.GetTripInfo(data.Id, email);
        }

        public async Task<TripInfoDTO> DeleteAction(long actionId, string email)
        {
            if (actionId == 0)
                return null;

            var tripAction = await _db.TripActions
                    .Include(ta => ta.Parent)
                    .ThenInclude(td => td.Parent)
                    .FirstOrDefaultAsync(ta => ta.Id == actionId);

            if (tripAction == null)
                return null;

            var trip = await _trRep.GetTrip(tripAction.Parent.Parent.Id);

            var editor = trip.Members.FirstOrDefault(m => m.Email == email);
            if (editor == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == editor.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            _db.TripActions.Remove(tripAction);
            await _db.SaveChangesAsync();

            return await _trRep.GetTripInfo(trip.Id, email);
        }

        public async Task<TripInfoDTO> DeleteTripDay(long tripDayId, string email)
        {
            var tripDayToDelete = await _db.TripDays
                    .Include(td => td.Parent)
                    .FirstOrDefaultAsync(td => td.Id == tripDayId);

            if (tripDayToDelete == null)
                return null;

            var trip = await _trRep.GetTrip(tripDayToDelete.Parent.Id);

            var editor = trip.Members.FirstOrDefault(m => m.Email == email);
            if (editor == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == editor.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            _db.TripDays.Remove(tripDayToDelete);
            await _db.SaveChangesAsync();

            return await _trRep.GetTripInfo(trip.Id, email);
        }

        public async Task<TripInfoDTO> EditAction(EditActionDTO data, string email)
        {
            if (data.ActionId == 0)
                return null;

            var tripAction = await _db.TripActions
                    .Include(ta => ta.Parent)
                    .ThenInclude(td => td.Parent)
                    .FirstOrDefaultAsync(ta => ta.Id == data.ActionId);

            if (tripAction == null)
                return null;

            var trip = await _trRep.GetTrip(tripAction.Parent.Parent.Id);

            var editor = trip.Members.FirstOrDefault(m => m.Email == email);
            if (editor == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == editor.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            if (tripAction.TimeOfAction != new TimeSpan(-1, 0, 0))
                tripAction.TimeOfAction = data.TimeOfAction;
            if (data.Name != null)
                tripAction.Name = data.Name;
            if (data.Description != null)
                tripAction.Description = data.Description;
            if (data.Location != null)
                tripAction.Location = data.Location;
            if (data.Files != null)
                throw new NotImplementedException("Добавление файлов пока недоступно!");

            await _db.SaveChangesAsync();

            return await _trRep.GetTripInfo(trip.Id, email);
        }

        public async Task<TripInfoDTO> EditTripDay(EditTripDayDTO data, string email)
        {
            var tripDayToEdit = await _db.TripDays
                .Include(td => td.Parent)
                .FirstOrDefaultAsync(td => td.Id == data.Id);

            if (tripDayToEdit == null)
                return null;

            var trip = await _trRep.GetTrip(tripDayToEdit.Parent.Id);

            var editor = trip.Members.FirstOrDefault(m => m.Email == email);
            if (editor == null)
                return null;

            var editorRole = trip.MemberRoles.FirstOrDefault(mr => mr.UserId == editor.Id);
            if (editorRole == null || editorRole.Role == Enums.TripRole.Viewer)
                return null;

            if (trip.TripDays.FirstOrDefault(td => td.Date == data.Date) != null)
                return null;

            tripDayToEdit.Date = data.Date;
            await _db.SaveChangesAsync();

            return await _trRep.GetTripInfo(trip.Id, email);
        }
    }
}
