using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelHelperBackend.DTOs;
using TravelHelperBackend.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelHelperBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        public AuthController(IAuthRepository repository)
        {
            _authRepository = repository;
        }
        // GET: api/<AuthController>
        [HttpGet("Register")]
        public async Task<IActionResult> RegisterAccount([FromForm] RegisterUserDTO registerData)
        {
            if ((await _authRepository.RegisterUser(registerData)) == false)
                return BadRequest("Ошибка в входных данных. Возможно, были не заполнены некоторые поля или аккаунт с таким Email и/или username был уже создан.");
            var authResponse = await _authRepository.AuthUser(new LoginDataDTO() { Email = registerData.Email, Password = registerData.Password } );
            if (authResponse == null)
                return Unauthorized("Аккаунт был зарегистрирован, но произошла ошибка при входе. Обратитесь к администратору.");
            else return Ok(authResponse);
        }

        [HttpGet("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] LoginDataDTO loginData)
        {
            var response = await _authRepository.AuthUser(loginData);
            if (response == null)
                return Unauthorized("Неправильный логин или пароль");
            else return Ok(response);
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
