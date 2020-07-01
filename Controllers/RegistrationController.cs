using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Model;
using ApiTask.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        IRegistration _repositoryRegistration;
        public RegistrationController(IRegistration repositoryRegistration)
        {
            _repositoryRegistration = repositoryRegistration;
        }

        // POST: api/Registration
        [HttpPost("[action]")]
        public async Task<IActionResult> SignUp([FromForm] RegisterUsers registerUsers, [FromHeader] string apiKey)
        {
            var user = new RegisterUsers
            {
                Username = registerUsers.Username,
                Password = PasswordEncryption.HashPassword(registerUsers.Password),
                Email = registerUsers.Email,
                ApiKey = registerUsers.ApiKey,
                SignupDate = DateTime.Now
            };

            if (await _repositoryRegistration.ValidateUser(user) == true)
            {
                return BadRequest("User already exists...");
            }

            await _repositoryRegistration.RegisterUser(user);
            return Ok("Registration was successful...");

        }

        
    }
}
