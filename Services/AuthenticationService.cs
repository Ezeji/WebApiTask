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
        private readonly ILogin _repositoryLogin;
        private readonly ITokenService _repositoryTokenService;

        public AuthenticationService(ILogin repositoryLogin, ITokenService repositoryTokenService)
        {
            _repositoryLogin = repositoryLogin;
            _repositoryTokenService = repositoryTokenService;
        }

        public async Task<string> Authenticate(LoginUsers loginUsers)
        {
            if (await _repositoryLogin.LoginUser(loginUsers) == true)
            {
                string securityToken = _repositoryTokenService.GetToken(loginUsers);
                return securityToken;
            }
            else
            {
                return "There's no such user...";
            }   
        }
    }
}
