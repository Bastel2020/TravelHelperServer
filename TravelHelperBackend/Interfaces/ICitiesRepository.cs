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
        public Task<List<PlaceCategoryDTO>> GetCityPlaces(int cityId);
        public Task<List<PlaceCategoryShortDTO>> GetCityPlacesShortInfo(int cityId);
        public Task<List<PlaceShortInfoDTO>> GetPlacesByCategoryShortInfo(int cityId, int category);
        public Task<PlaceInfoDTO> GetPlaceById(int placeId);
        public Task<List<PlaceInfoDTO>> SearchPlaces(SearchRequestDTO searchRequest);
    }
}
