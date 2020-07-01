using ApiTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Repository
{
    public interface IRegistration
    {
        Task RegisterUser(RegisterUsers registerUsers);
        Task<bool> ValidateUser(RegisterUsers registerUsers);
        Task<string> SendPasswordResetLinkEmail(string email, string link);
        Task<string> ResetUserPassword(ResetPassword resetPassword);
    }
}
