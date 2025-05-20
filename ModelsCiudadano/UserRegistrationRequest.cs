using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGESETT.Models
{
    public class UserRegistrationRequest
    {
        public string Cedula { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RolId { get; set; }
        public int? NacionalityId { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? GenderId { get; set; }
        public int? BloodTypeId { get; set; }
        public int? Height { get; set; }
        public int? WorkLocationId { get; set; }
        public int? CivilStatusId { get; set; }
        public string? NoAgente { get; set; }
        public int? StatusId { get; set; }
    }
}
