using ApiTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Services
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(RegisterUsers registerUsers);
    }
}
