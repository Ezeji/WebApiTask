using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Data;
using ApiTask.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Repository
{
    public class Registration : IRegistration
    {
        private ApiTaskDBContext _context;

        public Registration(ApiTaskDBContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(RegisterUsers registerUsers)
        {
            await _context.RegisterUser.AddAsync(registerUsers);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUser(RegisterUsers registerUsers)
        {
            var usercount = await _context.RegisterUser.Where(user => user.Username == registerUsers.Username
                                                            && user.Password == registerUsers.Password
                                                            && user.ApiKey == registerUsers.ApiKey)
                                                       .CountAsync();

            if (usercount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
