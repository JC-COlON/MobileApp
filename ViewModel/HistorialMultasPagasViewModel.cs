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
        private const string PaymentBaseUrl = "https://digesett.somee.com/api/Payments/GetByTicketId/";
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
            string cedula = ObtenerCedulaDelToken();
            if (string.IsNullOrEmpty(cedula))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
                return;
            }

            string url = $"{BaseUrl}?Cedula={cedula}&Estado=paid";

            try
            {
                IsLoading = true;

                using (HttpClient client = new HttpClient())
                {
                    string token = Preferences.Get("AuthToken", string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);

                        if (tickets != null && tickets.Count > 0)
                        {
                            await CargarPagosParaMultas(tickets); // ✅ Aquí se llama a los pagos

                            var orderedTickets = tickets
                                .OrderByDescending(ticket => DateTime.TryParse(ticket.TicketDate, out DateTime date) ? date : DateTime.MinValue)
                                .ToList();

                            Tickets = new ObservableCollection<Ticket>(orderedTickets);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("No hay multas", "Este ciudadano no tiene multas pagadas.", "OK");
                            await Task.Delay(200);
                            await Shell.Current.GoToAsync("//HomeViewCiudadano"); // 🚀 A donde tú quieras llevarlo

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("No hay multas", "Este ciudadano no tiene multas pagadas.", "OK");
                        await Task.Delay(200);
                        await Shell.Current.GoToAsync("//HomeViewCiudadano"); // 🚀 A donde tú quieras llevarlo

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



        private async Task CargarPagosParaMultas(List<Ticket> tickets)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string token = Preferences.Get("AuthToken", string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    int? userId = ObtenerUserIdDesdeToken();
                    if (userId == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener el userId desde el token.", "OK");
                        return;
                    }

                    string pagosUrl = $"https://digesett.somee.com/api/Payment/GetPagosByUsuario?userId={userId}";
                    HttpResponseMessage pagoResponse = await client.GetAsync(pagosUrl);

                    if (pagoResponse.IsSuccessStatusCode)
                    {
                        string pagoJson = await pagoResponse.Content.ReadAsStringAsync();
                        var pagos = JsonConvert.DeserializeObject<List<PaymentInfo>>(pagoJson) ?? new List<PaymentInfo>();



                        foreach (var ticket in tickets)
                        {
                            var pagoRelacionado = pagos.FirstOrDefault(p => p.TicketId == ticket.TicketId);

                            if (pagoRelacionado != null)
                            {
                                ticket.PaymentId = pagoRelacionado.PaymentId;
                                ticket.PaymentAmount = pagoRelacionado.Amount;
                                ticket.PaymentDateFormatted = pagoRelacionado.PaymentDate.ToString("dd/MM/yyyy");

                            }
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudieron obtener los pagos del usuario.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al obtener los pagos: {ex.Message}", "OK");
            }
        }




        private int ObtenerUserIdDesdeToken()
        {
            try
            {
                string token = Preferences.Get("AuthToken", string.Empty);
                if (string.IsNullOrEmpty(token)) return 0;

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener UserId: {ex.Message}");
                return 0;
            }
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

            public int PaymentId { get; set; }
            public decimal PaymentAmount { get; set; }
            public string PaymentDateFormatted { get; set; }



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

        public class PaymentInfo
        {
            [JsonProperty("paymentId")]
            public int PaymentId { get; set; }

            [JsonProperty("ticketId")]
            public int TicketId { get; set; }

            [JsonProperty("amount")]
            public decimal Amount { get; set; }

            [JsonProperty("paymentDate")]
            public DateTime PaymentDate { get; set; }

            [JsonProperty("formattedDate")]
            public string FormattedDate { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }
        }


        public class PagosResponse
        {
            [JsonProperty("$id")]
            public string Id { get; set; }

            [JsonProperty("$values")]
            public List<PaymentInfo> Values { get; set; }
        }




    }
}
