using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask.Model
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Logs { get; set; }
    }
}
