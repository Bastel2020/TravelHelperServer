using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database;
using TravelHelperBackend.DTOs;
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

        public async Task<List<PlaceInfoDTO>> GetCityPlaces(int cityId)
        {
            var city = await _db.Cities
                .Include(c => c.Places)
                .FirstOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
                return null;
            return city.Places
                .Select(p => new PlaceInfoDTO(p))
                .ToList();
        }

        public async Task<List<PlaceInfoDTO>> SearchPlaces(string searchRequest)
        {
            var places = _db.Places
                .Where(p => p.Name.ToLower().StartsWith(searchRequest.ToLower()));
            return places
                .Select(p => new PlaceInfoDTO(p))
                .ToList();
        }
    }
}
