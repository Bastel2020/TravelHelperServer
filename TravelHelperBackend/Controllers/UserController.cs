using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Interfaces;
using TravelHelperBackend.DTOs;

namespace TravelHelperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }
        // GET: api/<AuthController>
        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            var response = await _userRepository.GetProfile(User.Identity.Name);

            if (response == null)
                return Unauthorized("Невозможно получить информацию о пользователе. Возможно, вы не вошли в аккаунт.");

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented));
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> EditAccount([FromForm] EditUserProfileDTO data)
        {
            var response = await _userRepository.EditProfile(data, User.Identity.Name);

            if (response == null)
                return BadRequest("Невозможно обновнить информацию о пользователе. Возможно, вы не вошли в аккаунт.");

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDTO data)
        {
            var response = await _userRepository.ChangePassword(data, User.Identity.Name);

            if (response == false)
                return BadRequest("Невозможно обновить пароль. Проверьте правильность введённых данных.");

            return Ok("Пароль успешно обновлен.");
        }
    }
}