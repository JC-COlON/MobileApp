using System.Net.Http;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using DigesettAPP.Models;

namespace DigesettAPP.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        private bool _hayMultas;
        public bool HayMultas
        {
            get => _hayMultas;
            set
            {
                _hayMultas = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NoHayMultas)); // Asegúrate de notificar que la propiedad NoHayMultas cambió
            }
        }

        public bool NoHayMultas => !HayMultas;

        private ObservableCollection<Ticket> _listaDeMultas;
        public ObservableCollection<Ticket> ListaDeMultas
        {
            get => _listaDeMultas;
            set
            {
                _listaDeMultas = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            _httpClient = new HttpClient();
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

        public async Task LoadMultasPendientes()
        {
            try
            {
                var cedula = ObtenerCedulaDelToken();
                if (string.IsNullOrEmpty(cedula)) return;

                var url = $"https://digesett.somee.com/api/Ticket/Pending/{cedula}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var parsed = JsonDocument.Parse(json);
                    var lista = parsed.RootElement.GetProperty("$values");

                    var multasTemporales = new List<Ticket>();

                    foreach (var item in lista.EnumerateArray())
                    {
                        try
                        {
                            var multa = new Ticket
                            {
                                TicketId = item.GetProperty("ticketId").GetInt32(),
                                Name = item.GetProperty("name").GetString(),
                                LastName = item.GetProperty("lastName").GetString(),
                                Brand = item.GetProperty("brand").GetString(),
                                Model = item.GetProperty("model").GetString(),
                                LicensePlate = item.GetProperty("licensePlate").GetString(),
                                IncidentLocation = item.GetProperty("incidentLocation").GetString(),
                                TicketDate = item.GetProperty("ticketDate").GetString(),
                            };

                            multasTemporales.Add(multa);
                        }
                        catch (Exception ex)
                        {
                            await Shell.Current.DisplayAlert("Error", $"Error parseando multa: {ex.Message}", "OK");
                        }
                    }

                    // ✅ Ordenar y tomar las 2 más recientes
                    var ultimasDos = multasTemporales
                        .Where(m => DateTime.TryParse(m.TicketDate, out _))
                        .OrderByDescending(m => DateTime.Parse(m.TicketDate))
                        .Take(2)
                        .ToList();

                    ListaDeMultas = new ObservableCollection<Ticket>(ultimasDos);
                    HayMultas = ListaDeMultas.Any();
                }
                else
                {
                    HayMultas = false;
                }
            }
            catch (Exception ex)
            {
                HayMultas = false;
                await Shell.Current.DisplayAlert("Error", $"Excepción al cargar multas: {ex.Message}", "OK");
            }
        }


    }
}
