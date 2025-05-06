using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using DigesettAPP.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;

namespace DigesettAPP.ViewModels
{
    public class ListaMultaViewModel : BindableObject
    {
        private const string BaseUrl = "https://digesett.somee.com/api/Ticket/Pending";
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
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _rating;
        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged();
                }
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

        private string _comentario;
        public string Comentario
        {
            get => _comentario;
            set
            {
                _comentario = value;
                OnPropertyChanged();
            }
        }




        private double _calificacion;
        public double Calificacion
        {
            get => _calificacion;
            set
            {
                _calificacion = value;
                OnPropertyChanged();
            }
        }


        public ICommand IrAPagarMultaCommand { get; }
        public Command<Ticket> EnviarComentarioCommand { get; }
        public ListaMultaViewModel()
        {
            ToggleExpandCommand = new Command<Ticket>(ToggleExpand);
            CargarMultasCommand = new Command(async () => await CargarMultas());

            IrAPagarMultaCommand = new Command<Ticket>(async (ticket) =>
            {
                if (ticket != null)
                {
                    await Shell.Current.Navigation.PushAsync(new PagarMultaViewCiudadano(ticket.TicketId));
                }
            });

            EnviarComentarioCommand = new Command<Ticket>(async (ticket) =>
            {
                await EnviarReview(ticket);
            });
        }




        public ICommand CargarMultasCommand { get; }

        public ICommand ToggleExpandCommand { get; }
        private async void ToggleExpand(Ticket ticket)
        {
            ticket.IsExpanded = !ticket.IsExpanded;
            OnPropertyChanged(nameof(Tickets));

            if (ticket.IsExpanded)
            {
                await VerificarSiMultaYaFueCalificada(ticket);
            }
        }


        private async void IrAPagarMulta(Ticket ticket)
        {
            if (ticket == null)
                return;

            // Navegar a la página de pago con el ticketId
            await Shell.Current.GoToAsync($"pagarMultaCiudadano?ticketId={ticket.TicketId}");
        }




        private async Task CargarMultas()
        {
            string cedula = ObtenerCedulaDelToken();
            if (string.IsNullOrEmpty(cedula))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
                return;
            }

            string url = $"https://digesett.somee.com/api/Ticket/FilterOrGetTicket?Cedula={cedula}&Estado=pending";

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
                            var orderedTickets = tickets
                                .OrderByDescending(ticket => DateTime.TryParse(ticket.TicketDate, out DateTime date) ? date : DateTime.MinValue)
                                .ToList();

                            Tickets = new ObservableCollection<Ticket>(orderedTickets);

                            await Task.WhenAll(Tickets.Select(ticket => VerificarSiMultaYaFueCalificada(ticket)));
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Sin multas", "No tiene multas pendientes.", "OK");
                            await Shell.Current.GoToAsync("..");
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // 🔹 Mostrar mensaje personalizado si no hay multas
                        await App.Current.MainPage.DisplayAlert("Sin multas", "No tiene multas pendientes.", "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", $"No se pudieron cargar las multas. Código: {response.StatusCode}", "OK");
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





        private async Task EnviarReview(Ticket ticket)
        {
            // 🔵 Validar primero que el ticket tenga un agente válido
            if (ticket.Agente == null || ticket.Agente.UserId == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La multa no tiene asignado un agente válido para enviar la reseña.", "OK");
                return;
            }

            var review = new ReviewDTO
            {
                UserId = ObtenerUserIdDesdeToken(),
                TicketId = ticket.TicketId,
                AgentId = ticket.Agente.UserId,
                Rating = (int)ticket.Rating + 1,
                Comment = Comentario
            };

            if (review.UserId == 0 || review.TicketId == 0 || review.AgentId == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Datos incompletos para enviar la reseña.", "OK");
                return;
            }

            // Confirmación
            bool confirmar = await App.Current.MainPage.DisplayAlert("Confirmación", "¿Seguro que desea enviar la reseña?", "Sí", "No");
            if (!confirmar)
                return;

            IsLoading = true; // Mostrar spinner

            try
            {
                string json = JsonConvert.SerializeObject(review, Formatting.Indented);

                // 🟡 Mostrar el JSON en un alert para depuración


                using (HttpClient client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(120) // ⬅️ Aquí agregamos también el timeout
                })
                {
                    string token = Preferences.Get("AuthToken", string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://digesett.somee.com/api/Reviews/AddReviewForTicket", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Comentario = string.Empty;
                        Calificacion = 0;

                        await App.Current.MainPage.ShowPopupAsync(new PoputExitoReview());
                    }
                    else
                    {
                        var mensajeError = await response.Content.ReadAsStringAsync();

                        try
                        {
                            var error = JsonConvert.DeserializeObject<ErrorResponse>(mensajeError);

                            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest &&
                                error?.mensaje?.Contains("ya ha calificado", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                await App.Current.MainPage.DisplayAlert("Advertencia", error.mensaje, "OK");
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error", error?.mensaje ?? "No se pudo enviar la reseña.", "OK");
                            }
                        }
                        catch
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "No se pudo enviar la reseña. Intente nuevamente.", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al enviar reseña: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false; // Ocultar spinner
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

        private async Task VerificarSiMultaYaFueCalificada(Ticket ticket)
        {
            try
            {
                if (ticket.YaCalificada)
                    return; // Si ya se verificó, no hacemos nada

                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://digesett.somee.com/api/Reviews/GetTicketReviewStatus?ticketId={ticket.TicketId}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        bool yaCalificada = JsonConvert.DeserializeObject<bool>(json);
                        ticket.YaCalificada = yaCalificada;

                        // Eliminar el alert de "Multa calificada true"
                        // No mostramos nada aquí, solo actualizamos la propiedad
                    }
                    else
                    {
                        ticket.YaCalificada = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar review: {ex.Message}");
                ticket.YaCalificada = false;
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



        public class Ticket : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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

            private bool _yaCalificada;
            public bool YaCalificada
            {
                get => _yaCalificada;
                set
                {
                    if (_yaCalificada != value)
                    {
                        _yaCalificada = value;
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(EstaHabilitado)); // Se actualiza también la propiedad derivada
                    }
                }
            }


            // ✅ Nueva propiedad derivada

            public bool EstaHabilitado => !YaCalificada;


            // ➕ Propiedades calculadas
            public string FullName => $"{Name} {LastName}";

        public string ArticleFull => $" {Articulo?.ArticleNum} - {Articulo?.Description}";

        public string VehicleInfo => $" {Brand}/{Model} - Placa: {LicensePlate}";

        public string AgentInfo => $" {Agente?.Name} {Agente?.LastName} - No Agente: {AgentNumber}";

        public string FormattedTicketDate =>
            DateTime.TryParse(TicketDate, out var date) ? date.ToString("dd/MM/yyyy") : string.Empty;

        public string LocationInfo => $"  {Zone} ";

        public string PrecioInfo => $"{Articulo?.Price}";

        private double _rating;
        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(); // ✅ para refrescar si cambia visualmente
                }
            }
        }
    }


    public class TicketResponse
        {
            [JsonProperty("$id")]
            public string Id { get; set; }

            [JsonProperty("$values")]
            public Ticket[] Tickets { get; set; }
        }

        public class ReviewDTO
        {
            public int UserId { get; set; }
            public int Rating { get; set; }
            public int TicketId { get; set; }
            public int AgentId { get; set; }
            public string Comment { get; set; }
        }

        public class ErrorResponse
        {
            public string mensaje { get; set; }
        }



        public ICommand PayCommand => new Command<Ticket>((ticket) =>
        {
            // Aquí navegas a la pantalla de pago o inicias el proceso
            // Por ejemplo:
            Shell.Current.GoToAsync($"pagarMulta?ticketId={ticket.TicketId}");
        });


        public class ApiErrorResponse
        {
            [JsonProperty("mensaje")]
            public string mensaje { get; set; }
        }


    }
}
