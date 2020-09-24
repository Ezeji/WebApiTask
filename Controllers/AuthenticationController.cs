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
        ILogin _repositoryLogin;

        public AuthenticationController(ILogin repositoryLogin, IAuthenticationService repositoryAuthenticationService)
        {
            _repositoryLogin = repositoryLogin;
            _repositoryAuthenticationService = repositoryAuthenticationService;
        }

        // POST: api/Authentication
        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn([FromForm] LoginUsers loginUsers)
        {
            
            if (await _repositoryLogin.LoginUser(loginUsers) == true)
            {
                string token = await _repositoryAuthenticationService.Authenticate(loginUsers);
                var formattedToken = FormatToken.FormattedToken(token);
                return Ok(formattedToken);
            }
            else
            {
                string token = await _repositoryAuthenticationService.Authenticate(loginUsers);
                return NotFound(token);
            }

        }

        
    }
}
