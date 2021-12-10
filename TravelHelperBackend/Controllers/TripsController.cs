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
        private ITripDayRepository _tripDayRepository;
        public TripsController(ITripRepository tripRepository, ITripDayRepository tripDayRepository)
        {
            _tripRepository = tripRepository;
            _tripDayRepository = tripDayRepository;
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripDTO inputData)
        {
            var result = await _tripRepository.CreateTrip(inputData, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка в входных данных. Возможно, были не заполнены некоторые поля.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpGet("{id}/GenerateInvite")]
        public async Task<IActionResult> CreateTrip(int id)
        {
            var result = await _tripRepository.GenerateInviteCode(id, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка при генерации кода доступа. Возможно, вы не имеете доступа к этой поездке.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("Join")]
        public async Task<IActionResult> JoinByInvite([FromBody] string inviteCode)
        {
            var result = await _tripRepository.JoinByInviteCode(inviteCode, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка при при присоединении к поездке. Возможно, вы ошиблись при вводе кода.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("EditTrip")]
        public async Task<IActionResult> EditTrip([FromBody] EditTripInfoDTO dataToEdit)
        {
            var result = await _tripRepository.EditTripInfo(dataToEdit, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка при при изменении поездки. Возможно, у вас недостаточно прав или вы ошиблись при вводе данных.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripInfo(int id)
        {
            var result = await _tripRepository.GetTripInfo(id, User.Identity.Name);
            if (result == null)
                return Unauthorized("Ошибка доступа к поездке.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var result = await _tripRepository.DeleteTrip(id, User.Identity.Name);
            if (result == false)
                return Unauthorized("Ошибка при удалении поездки. Возможно, вы не можете удалять эту поездку.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("AddDay")]
        public async Task<IActionResult> AddTripDay([FromBody] AddTripDayDTO data)
        {
            var result = await _tripDayRepository.AddTripDay(data, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка создания дня. Возможно, вы не можете редактировать поездку.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpDelete("DeleteDay")]
        public async Task<IActionResult> DeleteTripDay([FromBody] long id)
        {
            var result = await _tripDayRepository.DeleteTripDay(id, User.Identity.Name);
            if (result == null)
                return Unauthorized("Ошибка удаления дня. Возможно, вы не можете редактировать поездку.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("EditTripDay")]
        public async Task<IActionResult> EditAction([FromBody] EditTripDayDTO data)
        {
            var result = await _tripDayRepository.EditTripDay(data, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка редактирования дня. Возможно, вы не можете редактировать поездку.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("AddAction")]
        public async Task<IActionResult> AddAction([FromBody] AddActionDTO data)
        {
            var result = await _tripDayRepository.AddAction(data, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка добавления события. Возможно, вы не можете редактировать поездку.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpDelete("DeleteAction")]
        public async Task<IActionResult> DeleteAction([FromBody] long id)
        {
            var result = await _tripDayRepository.DeleteAction(id, User.Identity.Name);
            if (result == null)
                return Unauthorized("Ошибка удаления события. Возможно, вы не можете редактировать поездку.");
            else return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("EditAction")]
        public async Task<IActionResult> EditAction([FromBody] EditActionDTO data)
        {
            var result = await _tripDayRepository.EditAction(data, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка редактирования события. Возможно, вы не можете редактировать поездку.");
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("Vote")]
        public async Task<IActionResult> VoteInPoll([FromBody] int tripId, int selectedVariant)
        {
            var result = await _tripRepository.VoteInPoll(tripId, selectedVariant, User.Identity.Name);
            if (result == null)
                return BadRequest("Ошибка при отправке голоса. Возможно, вы не имеете доступа к этому голосованию.");
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
