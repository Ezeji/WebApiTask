using ApiTask.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Data
{
    public class ApiTaskDBContext : DbContext
    {
        public ApiTaskDBContext(DbContextOptions<ApiTaskDBContext> options) : base(options)
        {
        }

        public DbSet<RegisterUsers> RegisterUser { get; set; }
        public DbSet<LoginUsers> LoginUser { get; set; }
        public DbSet<UserEvents> UserEvent { get; set; }
        public DbSet<Log> Log { get; set; }

    }
}
