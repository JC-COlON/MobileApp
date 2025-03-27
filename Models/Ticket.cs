using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        // Propiedades adicionales para mostrar la información formateada en la UI
        public string TicketNumber => $"Ticket # {TicketId}";

        public string TicketDetails => $"{Name} {LastName} - {LicensePlate} - {Brand}/{Model} - {FormattedDate}";

        public string FormattedDate => DateTime.TryParse(TicketDate, out DateTime date) ? date.ToString("dd/MM/yyyy") : TicketDate;
    }

    public class TicketResponse
    {
        [JsonProperty("$values")]
        public List<Ticket> Values { get; set; }
    }
}
