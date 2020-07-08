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
        public async Task<IActionResult> SignUp(RegisterUsers registerUsers)
        {
            if (await _repositoryRegistration.RegisterUser(registerUsers) == true)
            {
                return BadRequest("User already exists...");
            }
            else
            {
                return Ok("Registration was successful...");
            }
        }

        
    }
}
