using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigesettAPP.Models
{
    public class LoginResponse
    {
        public bool isSuccess { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }

}
