using ApiTask.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Services
{
    public interface ITokenService
    {
        string GetToken(LoginUsers loginUsers);
        SecurityTokenDescriptor GetTokenDescriptor(LoginUsers loginUsers);
    }
}
