using Newtonsoft.Json;
using System;

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

        [JsonProperty("incidentLocation")]
        public string IncidentLocation { get; set; }

        [JsonProperty("articulo")]
        public Articulo Articulo { get; set; }

        [JsonProperty("articleId")]
        public int ArticleId { get; set; }

        // ➕ Propiedad con la fecha formateada
        public string FormattedDate =>
            DateTime.TryParse(TicketDate, out DateTime date) ? date.ToString("dd/MM/yyyy") : string.Empty;

        // ➕ Propiedad combinada del artículo
        public string ArticuloFull =>
            Articulo != null
                ? $"{Articulo.ArticleNum} - {Articulo.Description}"
                : "Sin artículo";

        // ➕ Precio del artículo formateado
        public string ArticuloPrecio =>
            Articulo != null
                ? $"RD$ {Articulo.Price:N2}"
                : "RD$ 0.00";

        // Otras propiedades útiles
        public string TicketNumber => $"Ticket # {TicketId}";

        public string TicketDetails => $"{Name} {LastName} - {LicensePlate} - {Brand}/{Model} - {FormattedDate}";

        public string FullName => $"{Name} {LastName}";
        public string DatosVehiculos => $"{Brand} {Model}";
    }
}
