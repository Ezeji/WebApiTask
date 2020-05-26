using ApiTask.Model;
using ApiTask.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRegistration _repositoryRegistration;
        private readonly ITokenService _repositoryTokenService;

        public AuthenticationService(IRegistration repositoryRegistration, ITokenService repositoryTokenService)
        {
            _repositoryRegistration = repositoryRegistration;
            _repositoryTokenService = repositoryTokenService;
        }

        public async Task<string> Authenticate(RegisterUsers registerUsers)
        {
            if (await _repositoryRegistration.ValidateUser(registerUsers) == true)
            {
                string securityToken = _repositoryTokenService.GetToken(registerUsers);
                return securityToken;
            }
            else
            {
                return "There's no such user...";
            }   
        }
    }
}
