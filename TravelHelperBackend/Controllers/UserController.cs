using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Interfaces;
using TravelHelperBackend.DTOs;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            var response = await _userRepository.GetProfile(User.Identity.Name);

            if (response == null)
                return Unauthorized("Невозможно получить информацию о пользователе. Возможно, вы не вошли в аккаунт.");

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAccount([FromBody] EditUserProfileDTO data)
        {
            var response = await _userRepository.EditProfile(data, User.Identity.Name);

            if (response == null)
                return BadRequest("Невозможно обновнить информацию о пользователе. Возможно, вы не вошли в аккаунт.");

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented));
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO data)
        {
            var response = await _userRepository.ChangePassword(data, User.Identity.Name);

            if (response == false)
                return BadRequest("Невозможно обновить пароль. Проверьте правильность введённых данных.");

            return Ok("Пароль успешно обновлен.");
        }

        [Authorize]
        [HttpPost("UploadAvatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile uploadedFile)
        {
            
            var response = await _userRepository.UploadAvatar(uploadedFile, User.Identity.Name);

            if (response == false)
                return BadRequest("Невозможно обновить аватар. Размер файла не должен быть больше 1 Мб.");

            return Ok("Аватар успешно загружен.");
        }

        [Authorize]
        [HttpDelete("DeleteAvatar")]
        public async Task<IActionResult> DeleteAvatar()
        {

            var response = await _userRepository.DeleteAvatar(User.Identity.Name);

            if (response == false)
                return BadRequest("Невозможно обновить аватар. Файл должен быть изображением и не должен быть более 1 Мб.");

            return Ok("Аватар успешно удалён.");
        }

        [HttpGet("GetAvatar")]
        public async Task<IActionResult> GetAvatar([FromQuery] int userId)
        {

            var response = await _userRepository.GetAvatar(userId);

            if (response == null)
                return BadRequest("Невозможно получить аватар. Возможно, вы ошиблись в Id.");

            return File(response, "image/jpeg");
        }

        [Authorize]
        [HttpGet("Favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var response = await _userRepository.GetAllFavorites(User.Identity.Name);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("Favorites/AddOrRemove")]
        public async Task<IActionResult> AddOrRemoveFavorites([FromQuery] int placeId)
        {
            var response = await _userRepository.AddOrRemoveFromFavorites(User.Identity.Name, placeId);

            if (response)
                return Ok(response);
            else
                return BadRequest("Места с таким Id не найдено!");
        }

        [Authorize]
        [HttpGet("Favorites/Contains")]
        public async Task<IActionResult> ContainsInFavorites([FromQuery] int placeId)
        {
            var response = await _userRepository.IsInFavorites(User.Identity.Name, placeId);

            return Ok(response);
        }
    }
}