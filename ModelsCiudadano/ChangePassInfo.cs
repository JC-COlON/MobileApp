using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGESETT.Models
{
    public class ChangePassInfo
    {
        
        public string? identification { get; set; }
        public string? currentPassword { get; set; }
        public string? newPassword { get; set; }
        public string? confirmPassword { get; set; }

    }
}
