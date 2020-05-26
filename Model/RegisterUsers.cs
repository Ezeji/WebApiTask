using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Model
{
    public class RegisterUsers
    {
        [Key]
        public int UserId { get; set; }

        [FromForm]
        [Required(ErrorMessage ="Username is required...")]
        public string Username { get; set; }

        [FromForm]
        [Required(ErrorMessage = "Password is required...")]
        public string Password { get; set; }

        [FromHeader]
        [Required(ErrorMessage = "ApiKey is required...")]
        public string ApiKey { get; set; }

        public DateTime SignupDate { get; set; }
    }
}
