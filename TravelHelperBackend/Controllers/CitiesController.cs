using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet("{request}")]
        public async Task<IActionResult> GetCity(string request)
        {
            if(int.TryParse(request, out int cityId))
            {
                var responseById = await _citiesRepository.GetCityById(cityId);
                if (responseById == null)
                    return NotFound("Город с таким Id не найден!");
                return Ok(responseById);
            }
            var responseByString = await _citiesRepository.GetCityByName(request);
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

        [HttpGet("searchPlaces")]
        public async Task<IActionResult> GetPlaces([FromBody] string request)
        {
            var responseByString = await _citiesRepository.SearchPlaces(request);
            if (responseByString == null)
                return NotFound("Мест не найдено.");
            return Ok(responseByString);
        }
    }
}
