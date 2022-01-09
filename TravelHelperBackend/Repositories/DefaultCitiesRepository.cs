using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Enums;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Repositories
{
    public class DefaultCitiesRepository : ICitiesRepository
    {
        private DefaultDbContext _db;
        public DefaultCitiesRepository(DefaultDbContext context)
        {
            _db = context;
        }

        public async Task<CityInfoDTO> GetCityById(int cityId)
        {
            var city = await _db.Cities
                .Include(c => c.MainPhoto)
                .Include(c => c.Photos)
                .FirstOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
                return null;
            return new CityInfoDTO(city);
        }

        public async Task<CityInfoDTO> GetCityByName(string name)
        {
            var city = await _db.Cities
                .Include(c => c.MainPhoto)
                .Include(c => c.Photos)
                .SingleOrDefaultAsync(c => c.Name.ToLower().StartsWith(name.ToLower()));
            if (city == null)
                return null;
            return new CityInfoDTO(city);
        }

        public async Task<List<PlaceCategoryDTO>> GetCityPlaces(int cityId)
        {
            var city = await _db.Cities
                .Include(c => c.Places)
                .ThenInclude(p => p.MainPhoto)
                .Include(c => c.Places)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
                return null;
            return city.Places
                .GroupBy(p => p.PlaceCategory)
                .Select(p => new PlaceCategoryDTO(p))
                .ToList();
        }

        public async Task<List<PlaceCategoryShortDTO>> GetCityPlacesShortInfo(int cityId)
        {
            var city = await _db.Cities
                .Include(c => c.Places)
                .FirstOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
                return null;
            return city.Places
                .GroupBy(p => p.PlaceCategory)
                .Select(p => new PlaceCategoryShortDTO(p))
                .ToList();
        }

        public async Task<PlaceInfoDTO> GetPlaceById(int placeId)
        {
            var place = await _db.Places
                .Include(p => p.City)
                .Include(p => p.MainPhoto)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == placeId);
            if (place == null)
                return null;
            return new PlaceInfoDTO(place);
        }

        public async Task<List<PlaceShortInfoDTO>> GetPlacesByCategoryShortInfo(int cityId, int category)
        {
            var places = await _db.Places
                .Include(p => p.City)
                .Where(p => p.City.Id == cityId && (int)p.PlaceCategory == category)
                .ToListAsync();

            if (places == null)
                return null;

            return places.Select(p => new PlaceShortInfoDTO(p))
                .ToList();
        }

        public async Task<List<PlaceInfoDTO>> SearchPlaces(SearchRequestDTO searchRequest)
        {
            var places = _db.Places
                .Include(p => p.City)
                .Include(p => p.MainPhoto)
                .Include(p => p.Photos)
                .Where(p => p.Name.ToLower().Contains(searchRequest.Name.ToLower()));

            if (searchRequest.CityId != 0)
                places = places.Where(p => p.City.Id == searchRequest.CityId);

            if (searchRequest.placeCategory != 0)
            {
                if (searchRequest.placeCategory == 1)
                    places = places.Where(p => p.PlaceCategory == PlaceCategory.Достопримечательности);
                else if (searchRequest.placeCategory == 2)
                    places = places.Where(p => p.PlaceCategory == PlaceCategory.Парки);
                else if (searchRequest.placeCategory == 3)
                    places = places.Where(p => p.PlaceCategory == PlaceCategory.ТЦ);
                else if (searchRequest.placeCategory == 4)
                    places = places.Where(p => p.PlaceCategory == PlaceCategory.Развлечения);
                else if (searchRequest.placeCategory == 5)
                    places = places.Where(p => p.PlaceCategory == PlaceCategory.Пляжи);
                else
                    return null;

            }
            if (places.Count() == 0)
                return null;
            return await places
                .Select(p => new PlaceInfoDTO(p))
                .ToListAsync();
        }
    }
}
