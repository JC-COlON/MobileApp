using System.Net.Http;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using DigesettAPP.Models;
using System.Windows.Input;
using DigesettAPP.Views;

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
                    OnPropertyChanged(nameof(NoHayMultas));
                    OnPropertyChanged(nameof(MostrarMultas));
                    OnPropertyChanged(nameof(MostrarTips));
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
                    OnPropertyChanged(nameof(NoHayMultas));
                    OnPropertyChanged(nameof(MostrarMultas));
                    OnPropertyChanged(nameof(MostrarTips));
                }
            }

            // Esta propiedad sigue siendo útil
            public bool NoHayMultas => !HayMultas && !IsLoading;

            // Propiedades nuevas para el control de visibilidad
            public bool MostrarMultas => HayMultas && !IsLoading;
            public bool MostrarTips => !HayMultas && !IsLoading;

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

            // Constructor y lógica de carga...
        



        public HomeViewModel()
        {
            _httpClient = new HttpClient();
        }

        public ICommand VerDetalleMultaCommand => new Command<Ticket>(async (ticket) =>
        {
            if (ticket == null) return;

            IsLoading = true;

            // Esperar brevemente para dejar que la UI actualice el spinner
            await Task.Yield();
            await Task.Delay(300); // Esto da tiempo a que el spinner se muestre correctamente

            // Navegación
            await Shell.Current.GoToAsync($"{nameof(ListaMultasCiudadano)}?ticketId={ticket.TicketId}");

            // No se pone IsLoading = false porque cambiarás de página
        });




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
                IsLoading = true;

                var cedula = ObtenerCedulaDelToken();
                if (string.IsNullOrEmpty(cedula)) return;

                var url = $"https://a775-200-215-234-53.ngrok-free.app/api/Ticket/Pending/{cedula}";
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

                    var ultimasDos = multasTemporales
                        .Where(m => DateTime.TryParse(m.TicketDate, out _))
                        .OrderByDescending(m => DateTime.Parse(m.TicketDate))
                        .Take(5)
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
            finally
            {
                IsLoading = false;
            }
        }


    }
}
