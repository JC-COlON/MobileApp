using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DigesettAPP.ViewModel
{
    public  class HistorialMultasPagasViewModel : BindableObject
    {
        private const string BaseUrl = "https://digesett.somee.com/api/Ticket/FilterOrGetTicket";
        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get => _tickets;
            set
            {
                _tickets = value;
                OnPropertyChanged();
            }
        }
        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }




        public HistorialMultasPagasViewModel()
        {
            ToggleExpandCommand = new Command<Ticket>(ToggleExpand);  // Cambié a Command<Ticket> para pasar el objeto actual
            CargarMultasCommand = new Command(async () => await CargarMultas());

        }



        public ICommand CargarMultasCommand { get; }

        public ICommand ToggleExpandCommand { get; }
        private void ToggleExpand(Ticket ticket)
        {
            ticket.IsExpanded = !ticket.IsExpanded;  // Cambia el estado expandido del ticket
            OnPropertyChanged(nameof(Tickets));  // Notificar que la lista de tickets ha cambiado
        }


        private async Task CargarMultas()
        {
            // Extraer la cédula del token
            string cedula = ObtenerCedulaDelToken();
            if (string.IsNullOrEmpty(cedula))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
                return;
            }

            // Construir la URL correctamente con los parámetros Cedula y Estado
            string url = $"{BaseUrl}?Cedula={cedula}&Estado=paid";  // Ahora incluye ambos parámetros

            try
            {
                IsLoading = true;

                using (HttpClient client = new HttpClient())
                {
                    string token = Preferences.Get("AuthToken", string.Empty);  // Se obtiene el token de autorización
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserializamos directamente el array de tickets
                        var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);

                        if (tickets != null && tickets.Count > 0)
                        {
                            // Ordenamos los tickets por fecha de manera descendente
                            var orderedTickets = tickets
                                .OrderByDescending(ticket => DateTime.TryParse(ticket.TicketDate, out DateTime date) ? date : DateTime.MinValue)
                                .ToList();

                            // Asignamos la colección ordenada a la propiedad Tickets
                            Tickets = new ObservableCollection<Ticket>(orderedTickets);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("No hay multas", "Este ciudadano no tiene multas pendientes.", "OK");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudieron cargar las multas.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

       



        // Método para obtener la cédula desde el token
        private string ObtenerCedulaDelToken()
        {
            try
            {
                // Obtener el token de autenticación
                string token = Preferences.Get("AuthToken", string.Empty);
                if (string.IsNullOrEmpty(token)) return null;

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                // Buscar el claim de la cédula (serialnumber)
                var cedulaClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");
                return cedulaClaim?.Value;
            }
            catch (Exception ex)
            {
                // Si hay algún error al obtener la cédula, mostramos un error
                Console.WriteLine($"Error al obtener la cédula del token: {ex.Message}");
                return null;
            }
        }

        private async Task<List<Payment>> ObtenerPagosPorUsuario(string userId)
        {
            try
            {
                string token = Preferences.Get("AuthToken", string.Empty);
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string pagosUrl = $"https://localhost:7277/api/Payment/GetPagosByUsuario?userId={userId}";
                    HttpResponseMessage response = await client.GetAsync(pagosUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var pagos = JsonConvert.DeserializeObject<List<Payment>>(jsonResponse);
                        return pagos;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo pagos: {ex.Message}");
            }

            return new List<Payment>();
        }


        public class VehicleType
        {
            [JsonProperty("$id")]
            public string Id { get; set; }

            public int VehicleTypeId { get; set; }
            public string Name { get; set; }
        }

        public class Articulo
        {
            [JsonProperty("$id")]
            public string Id { get; set; }

            public string ArticleNum { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int ArticleId { get; set; }
        }

        public class Agente
        {
            [JsonProperty("$id")]
            public string Id { get; set; }

            public int UserId { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
        }

        public class Ticket
        {
            public int TicketId { get; set; }
            public string Zone { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Identification { get; set; }
            public string LicensePlate { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string IncidentLocation { get; set; }
            public string Observations { get; set; }
            public string AgentNumber { get; set; }
            public string TicketDate { get; set; }
            public string Status { get; set; }

            public Articulo Articulo { get; set; }
            public Agente Agente { get; set; }

            public bool IsExpanded { get; set; }

            // ➕ Propiedades calculadas
            public string FullName => $"{Name} {LastName}";

            public string ArticleFull => $" {Articulo?.ArticleNum} - {Articulo?.Description}";

            public string VehicleInfo => $" {Brand}/{Model} - Placa: {LicensePlate}";

            public string AgentInfo => $" {Agente?.Name} {Agente?.LastName} - No Agente: {AgentNumber}";

            public string FormattedTicketDate =>
                DateTime.TryParse(TicketDate, out var date) ? date.ToString("dd/MM/yyyy") : string.Empty;

            public string LocationInfo => $"  {Zone} ";

            public string PrecioInfo => $"{Articulo?.Price}";

            public int? PaymentId { get; set; }
            public decimal? PaymentAmount { get; set; }
            public DateTime? PaymentDate { get; set; }

            public string PaymentDateFormatted =>
                PaymentDate.HasValue ? PaymentDate.Value.ToString("dd/MM/yyyy") : string.Empty;


        }


        public class TicketResponse
        {
            [JsonProperty("$id")]
            public string Id { get; set; }

            [JsonProperty("$values")]
            public Ticket[] Tickets { get; set; }
        }

 

        public class ErrorResponse
        {
            public string mensaje { get; set; }
        }


        public class Payment
        {
            public int PaymentId { get; set; }
            public int TicketId { get; set; }
            public decimal Amount { get; set; }
            public DateTime PaymentDate { get; set; }
        }


    }
}
