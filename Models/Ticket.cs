using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigesettAPP.Models
{
    public class Ticket
    {
        [JsonProperty("ticketId")]
        public int TicketId { get; set; }

        [JsonProperty("zone")]
        public string Zone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("licensePlate")]
        public string LicensePlate { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("ticketDate")]
        public string TicketDate { get; set; }

        // Esta propiedad se usa para mostrar los datos formateados
        public string FormattedInfo { get; set; }
    }


    public class TicketResponse
    {
        [JsonProperty("$values")]
        public List<Ticket> Values { get; set; }
    }
}

