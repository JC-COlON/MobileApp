//using System.Net.Http;
//using System.Text.Json;
//using System.Collections.ObjectModel;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.Maui.Storage;
//using CommunityToolkit.Mvvm.ComponentModel;

//namespace DigesettAPP.ViewModels
//{
//    public partial class HomeViewModel : ObservableObject
//    {
//        private readonly HttpClient _httpClient;

//        [ObservableProperty]
//        private string formattedCardNumber = "****-****-****-****";

//        [ObservableProperty]
//        private string expirationDate = "MM/YY";

//        public HomeViewModel()
//        {
//            _httpClient = new HttpClient();
//            LoadCreditCard();
//        }

//        private async void LoadCreditCard()
//        {
//            try
//            {
//                var token = Preferences.Get("AuthToken", string.Empty);
//                var handler = new JwtSecurityTokenHandler();
//                var jwtToken = handler.ReadJwtToken(token);

//                var cedula = jwtToken.Claims.FirstOrDefault(c =>
//                    c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber")?.Value;

//                if (string.IsNullOrWhiteSpace(cedula))
//                    return;

//                var response = await _httpClient.GetAsync($"https://digesett.somee.com/api/CreditCard/GetCreditCardsByCedula?cedula={cedula}");

//                if (response.IsSuccessStatusCode)
//                {
//                    var json = await response.Content.ReadAsStringAsync();
//                    var parsed = JsonDocument.Parse(json);
//                    var card = parsed.RootElement
//                        .GetProperty("$values")[0];

//                    var cardNumber = card.GetProperty("cardNumber").ToString();
//                    var expiration = card.GetProperty("expirationDate").GetString();

//                    FormattedCardNumber = MaskCardNumber(cardNumber);
//                    ExpirationDate = expiration ?? "MM/YY";
//                }
//            }
//            catch
//            {
//                // Puedes manejar errores aquí
//            }
//        }

//        private string MaskCardNumber(string cardNumber)
//        {
//            if (cardNumber.Length < 4)
//                return "****-****-****-****";

//            var lastFour = cardNumber.Substring(cardNumber.Length - 4);
//            return $"****-****-****-{lastFour}";
//        }
//    }
//}




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

        // ✅ Propiedades con backing field para evitar problemas de generación
        private ObservableCollection<Ticket> _listaDeMultas = new();
        public ObservableCollection<Ticket> ListaDeMultas
        {
            get => _listaDeMultas;
            set => SetProperty(ref _listaDeMultas, value);
        }

        private bool _hayMultas;
        public bool HayMultas
        {
            get => _hayMultas;
            set => SetProperty(ref _hayMultas, value);
        }

        public HomeViewModel()
        {
            _httpClient = new HttpClient();
            LoadMultasPendientes();
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

        private async void LoadMultasPendientes()
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
                        var multa = new Ticket
                        {
                            TicketId = item.GetProperty("ticketId").GetInt32(),
                            Name = item.GetProperty("name").GetString(),
                            LastName = item.GetProperty("lastName").GetString(),
                            Brand = item.GetProperty("brand").GetString(),
                            Model = item.GetProperty("model").GetString(),
                            LicensePlate = item.GetProperty("licensePlate").GetString(),
                            IncidentLocation = item.GetProperty("incidentLocation").GetString(),
                            TicketDate = item.GetProperty("ticketDate").GetString()
                        };

                        multasTemporales.Add(multa);
                    }

                    // Tomar solo las 2 últimas multas, ordenadas por fecha descendente
                    var ultimasDos = multasTemporales
                        .OrderByDescending(m => DateTime.TryParse(m.TicketDate, out var date) ? date : DateTime.MinValue)
                        .Take(2);

                    ListaDeMultas.Clear();
                    foreach (var multa in ultimasDos)
                        ListaDeMultas.Add(multa);

                    HayMultas = ListaDeMultas.Any();
                }
                else
                {
                    HayMultas = false;
                }
            }
            catch
            {
                HayMultas = false;
            }
        }

    }
}
