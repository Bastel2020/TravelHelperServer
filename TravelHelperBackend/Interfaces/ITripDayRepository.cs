using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface ITripDayRepository
    {
        public Task<TripInfoDTO> AddTripDay(AddTripDayDTO data, string email);
        public Task<TripInfoDTO> DeleteTripDay(long tripDayId, string email);
        public Task<TripInfoDTO> EditTripDay(EditTripDayDTO data, string email);
        public Task<ActionInfoDTO> GetActionInfo(long actionId, string email);
        public Task<TripInfoDTO> AddAction(AddActionDTO data, string email);
        public Task<TripInfoDTO> DeleteAction(long actionId, string email);
        public Task<TripInfoDTO> EditAction(EditActionDTO data, string email);
        public Task<ActionInfoDTO> CreatePoll(CreatePollDTO data, string email);
        public Task<ActionInfoDTO> DeletePoll(int pollId, string email);
        public Task<ActionInfoDTO> VoteInPoll(int pollId, int selectedOption, string email);
    }
}
