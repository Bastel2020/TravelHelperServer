using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class TripsController : ControllerBase
    {
        private ITripRepository _tripRepository;
        public TripsController(ITripRepository repository)
        {
            _tripRepository = repository;
        }

        [Authorize]
        [HttpGet("Create")]
        public async Task<IActionResult> CreateTrip([FromForm] CreateTripDTO inputData)
        {
            var result = await _tripRepository.CreateTrip(inputData, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка в входных данных. Возможно, были не заполнены некоторые поля.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpGet("GenerateInvite")]
        public async Task<IActionResult> CreateTrip([FromForm] int TripId)
        {
            var result = await _tripRepository.GenerateInviteCode(TripId, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка при генерации кода доступа. Возможно, вы не имеете доступа к этой поездке.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
