using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTask.Data;
using ApiTask.Model;

namespace ApiTask.Repository
{
    public class Login : ILogin
    {
        private ApiTaskDBContext _context;

        public Login(ApiTaskDBContext context)
        {
            _context = context;
        }

        public async Task LoginUser(LoginUsers loginUsers)
        {
            await _context.LoginUser.AddAsync(loginUsers);
            await _context.SaveChangesAsync();
        }
    }
}
