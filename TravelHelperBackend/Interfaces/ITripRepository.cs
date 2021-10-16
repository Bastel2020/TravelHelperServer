using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface ITripRepository
    {
        public Task<TripInfoDTO> CreateTrip(CreateTripDTO data, string email);
        public Task<TripInfoDTO> CreateTripWithoutDates(CreateTripWithoutDatesDTO data, string email);
        public Task<bool> EditTripInfo(EditTripInfoDTO data, string email);
        public Task<bool> AddTripDay(AddTripDayDTO data, string email);
        public Task<TripInfoDTO> GetTripInfo(int tripId, string email);
        public Task<TripInfoDTO> GenerateInviteCode(int tripId, string email);
        public Task<TripInfoDTO> JoinByInviteCode(string invite, string email);
        public Task<bool> AddUserToTrip(string emailToInvite, int tripId, string email);
        public Task<bool> AddUserToTripWithoutСheck(Trip tripToAdd, string emailToAdd);
    }
}
