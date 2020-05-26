using ApiTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Repository
{
    public interface ILogin
    {
        Task LoginUser(LoginUsers loginUsers);
    }
}
