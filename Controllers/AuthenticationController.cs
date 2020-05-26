using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Model;
using ApiTask.Repository;
using ApiTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _repositoryAuthenticationService;
        IRegistration _repositoryRegistration;
        ILogin _repositoryLogin;

        public AuthenticationController(IRegistration repositoryRegistration, ILogin repositoryLogin, IAuthenticationService repositoryAuthenticationService)
        {
            _repositoryRegistration = repositoryRegistration;
            _repositoryLogin = repositoryLogin;
            _repositoryAuthenticationService = repositoryAuthenticationService;
        }

        // POST: api/Authentication
        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn([FromForm] RegisterUsers registerUsers, [FromHeader] string apiKey)
        {
            var registerUser = new RegisterUsers
            {
                Username = registerUsers.Username,
                Password = PasswordEncryption.HashPassword(registerUsers.Password),
                ApiKey = registerUsers.ApiKey
            };

            if (await _repositoryRegistration.ValidateUser(registerUser) == true)
            {
                var loginUser = new LoginUsers
                {
                    Username = registerUser.Username,
                    Password = registerUser.Password,
                    ApiKey = registerUser.ApiKey,
                    LoginDate = DateTime.Now
                };
                await _repositoryLogin.LoginUser(loginUser);

                string token = await _repositoryAuthenticationService.Authenticate(registerUser);
                var formattedToken = FormatToken.FormattedToken(token);
                return Ok(formattedToken);
            }
            else
            {
                string token = await _repositoryAuthenticationService.Authenticate(registerUser);
                return NotFound(token);
            }

            
        }

        
    }
}
