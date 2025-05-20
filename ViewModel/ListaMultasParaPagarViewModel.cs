using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DigesettAPP.Models;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace DigesettAPP.ViewModels
{
    public class ListaMultasParaPagarViewModel : BindableObject
    {
        private const string BaseUrl = "https://digesett.somee.com/api/Ticket/Pending";

        private ObservableCollection<Ticket> _multas;
        public ObservableCollection<Ticket> Multas
        {
            get => _multas;
            set
            {
                _multas = value;
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

        public ICommand CargarMultasCommand { get; }

        public ListaMultasParaPagarViewModel()
        {
            CargarMultasCommand = new Command(async () => await CargarMultas());
        }

        private async Task CargarMultas()
        {
            string cedula = ObtenerCedulaDelToken();

            if (string.IsNullOrEmpty(cedula))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
                return;
            }

            string url = $"{BaseUrl}/{cedula}";

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

                        if (tickets != null && tickets.Any())
                        {
                            var orderedTickets = tickets
                                .OrderByDescending(t => DateTime.TryParse(t.TicketDate, out var date) ? date : DateTime.MinValue)
                                .ToList();

                            Multas = new ObservableCollection<Ticket>(orderedTickets);
                        }
                        else
                        {
                            Multas = new ObservableCollection<Ticket>();
                            await App.Current.MainPage.DisplayAlert("Sin multas", "No tienes multas pendientes por pagar.", "OK");
                            await Shell.Current.GoToAsync("..");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        string errorMessage = "Este Usuario No tiene Multas Pendientes.";
                        await App.Current.MainPage.DisplayAlert("Sin multas", errorMessage, "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string mensajeApi = "No tienes multas pendientes por pagar.";
                        try
                        {
                            dynamic errorObj = JsonConvert.DeserializeObject(jsonResponse);
                            if (errorObj?.message != null)
                            {
                                mensajeApi = errorObj.message;
                            }
                        }
                        catch
                        {
                            // ignorar si falla el parseo
                        }

                        await App.Current.MainPage.DisplayAlert("Sin multas", mensajeApi, "OK");
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudieron cargar las multas.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }




        private string ObtenerCedulaDelToken()
        {
            try
            {
                string token = Preferences.Get("AuthToken", string.Empty);
                if (string.IsNullOrEmpty(token)) return null;

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var cedulaClaim = jsonToken?.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");

                return cedulaClaim?.Value;
            }
            catch
            {
                return null;
            }
        }
    }

    public class Ticket
    {
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public string AgentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int ArticuloInfringido { get; set; }
        public string Zona { get; set; }
        public string LugarHecho { get; set; }
        public string Estado { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        
        public string IncidentLocation { get; set; }
        public string TicketDate { get; set; }
        public string? FechaVencimiento { get; set; }
        public string? UpdatedAt { get; set; }
        public string? DeletedAt { get; set; }

        public Articulo Articulo { get; set; }
        public string Descripcion => $"Infracción #{TicketId} en zona {Zona}";
        public string Fecha => DateTime.TryParse(TicketDate, out var date) ? date.ToString("dd/MM/yyyy") : "Sin fecha";

        public string PrecioInfo => $"{Articulo?.Price}";

        public string FormattedTicketDate =>
                DateTime.TryParse(TicketDate, out var date) ? date.ToString("dd/MM/yyyy") : string.Empty;


        public string FormattedTicketDatee =>
    DateTime.TryParse(TicketDate, out var date)
        ? date.ToString("dd/MM/yyyy hh:mm tt")
        : string.Empty;



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
}
