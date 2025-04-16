using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DIGESETT.Models
{
    public class LoginResponse
    {
        public string? Token { get; set; }

        [JsonProperty("user")]  
        public UserBasicInfo? UserDetail { get; set; }
    }
}
