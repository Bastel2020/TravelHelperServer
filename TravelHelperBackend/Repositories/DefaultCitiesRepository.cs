﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PlaceCategoryDTO>> GetCityPlaces(int cityId)
        {
            var city = await _db.Cities
                .Include(c => c.Places)
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

        public async Task<List<PlaceInfoDTO>> SearchPlaces(string searchRequest)
        {
            var places = _db.Places
                .Where(p => p.Name.ToLower().StartsWith(searchRequest.ToLower()));
            return await places
                .Select(p => new PlaceInfoDTO(p))
                .ToListAsync();
        }
    }
}
