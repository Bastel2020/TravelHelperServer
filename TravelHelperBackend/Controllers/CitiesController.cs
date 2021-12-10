using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;

namespace TravelHelperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private ICitiesRepository _citiesRepository;
        public CitiesController(ICitiesRepository repository)
        {
            _citiesRepository = repository;
        }

        [HttpGet("{idOrRequest}")]
        public async Task<IActionResult> GetCity(string idOrRequest)
        {
            if(int.TryParse(idOrRequest, out int cityId))
            {
                var responseById = await _citiesRepository.GetCityById(cityId);
                if (responseById == null)
                    return NotFound("Город с таким Id не найден!");
                return Ok(responseById);
            }
            var responseByString = await _citiesRepository.GetCityByName(idOrRequest);
            if (responseByString == null)
                return NotFound("Город с таким названием не найден!");
            return Ok(responseByString);
        }

        [HttpGet("{id}/places")]
        public async Task<IActionResult> GetPlaces(int id)
        {
            var responseByString = await _citiesRepository.GetCityPlaces(id);
            if (responseByString == null)
                return NotFound("Город с таким Id не найден!");
            return Ok(responseByString);
        }

        [HttpGet("{id}/placesShort")]
        public async Task<IActionResult> GetPlacesShortInfo(int id)
        {
            var responseByString = await _citiesRepository.GetCityPlacesShortInfo(id);
            if (responseByString == null)
                return NotFound("Город с таким Id не найден!");
            return Ok(responseByString);
        }

        [HttpGet("{cityId}/placesByCategory/{categoryId}")]
        public async Task<IActionResult> GetPlacesByCategoryShortInfo(int cityId, int categoryId)
        {
            var response = await _citiesRepository.GetPlacesByCategoryShortInfo(cityId, categoryId);
            if (response == null)
                return NotFound("Не найден город и/или категория с таким Id!");
            return Ok(response);
        }

        [HttpPost("searchPlaces")]
        public async Task<IActionResult> GetPlaces([FromBody] SearchRequestDTO request)
        {
            var responseByString = await _citiesRepository.SearchPlaces(request);
            if (responseByString == null)
                return NotFound("Мест не найдено.");
            return Ok(responseByString);
        }

        [HttpGet("places/{placeId}")]
        public async Task<IActionResult> GetPlace(int placeId)
        {
            var response = await _citiesRepository.GetPlaceById(placeId);
            if (response == null)
                return NotFound("Место с таким Id не найдено.");
            return Ok(response);
        }
    }
}
