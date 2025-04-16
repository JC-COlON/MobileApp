
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGESETT.Models {

    public class UserBasicInfo
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Rol { get; set; }
    }

    public enum RoleDetails
    {
        [Description("Usuario")]
        Usuario,

        [Description("Agente")]
        Agente,

        [Description("Administrador")]
        Administrador
    }

}
