using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Interfaces
{
    public interface ICitiesRepository
    {
        public Task<CityInfoDTO> GetCityById(int cityId);
        public Task<CityInfoDTO> GetCityByName(string name);
        public Task<List<PlaceInfoDTO>> GetCityPlaces(int cityId);
        public Task<List<PlaceInfoDTO>> SearchPlaces(string searchRequest);
    }
}
