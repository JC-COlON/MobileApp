using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DIGESETT.Models
{

    public class SearchInfo
    {
        public int ticketId { get; set; }
        public string Zone { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string identification { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string IncidentLocation { get; set; }
        public string Observations { get; set; }
        public string AgentNumber { get; set; }
        public DateTime TicketDate { get; set; }
        public int Hidden { get; set; }
        public string Status { get; set; }

        public Articulo Articulo { get; set; }
        public Agente Agente { get; set; }
    }

    public class Articulo
    {
        public string ArticleNum { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        // Propiedad extra para binding en la UI

    }

    public class Agente
    {
        public int userId { get; set; } 
        public string? name { get; set; }
        public string? lastName { get; set; }
    }

}

