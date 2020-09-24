using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Data;
using ApiTask.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Repository
{
    public class Login : ILogin
    {
        private ApiTaskDBContext _context;

        public Login(ApiTaskDBContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginUser(LoginUsers loginUsers)
        {
            var usercount = await _context.RegisterUser.Where(user => user.Username == loginUsers.Username
                                                            && user.Password == PasswordEncryption.HashPassword(loginUsers.Password)
                                                            && user.ApiKey == loginUsers.ApiKey)
                                                       .CountAsync();

            if (usercount > 0)
            {
                var loginUser = new LoginUsers
                {
                    Username = loginUsers.Username,
                    Password = PasswordEncryption.HashPassword(loginUsers.Password),
                    ApiKey = loginUsers.ApiKey,
                    LoginDate = DateTime.Now
                };

                await _context.LoginUser.AddAsync(loginUser);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }

            
        }
    }
}
