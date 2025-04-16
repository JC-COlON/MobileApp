using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGESETT.Models
{
    public class LoginRequest
    {
        public string userId { get; set; }
        public string? Cedula { get; set; }
        public string? Password { get; set; }
    }
}
