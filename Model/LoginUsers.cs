using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Model
{
    public class LoginUsers
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ApiKey { get; set; }

        public DateTime LoginDate { get; set; }
    }
}
