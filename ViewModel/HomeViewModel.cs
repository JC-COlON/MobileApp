using System.Net.Http;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DigesettAPP.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string formattedCardNumber = "****-****-****-****";

        [ObservableProperty]
        private string expirationDate = "MM/YY";

        public HomeViewModel()
        {
            _httpClient = new HttpClient();
            LoadCreditCard();
        }

        private async void LoadCreditCard()
        {
            try
            {
                var token = Preferences.Get("AuthToken", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var cedula = jwtToken.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber")?.Value;

                if (string.IsNullOrWhiteSpace(cedula))
                    return;

                var response = await _httpClient.GetAsync($"https://digesett.somee.com/api/CreditCard/GetCreditCardsByCedula?cedula={cedula}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var parsed = JsonDocument.Parse(json);
                    var card = parsed.RootElement
                        .GetProperty("$values")[0];

                    var cardNumber = card.GetProperty("cardNumber").ToString();
                    var expiration = card.GetProperty("expirationDate").GetString();

                    FormattedCardNumber = MaskCardNumber(cardNumber);
                    ExpirationDate = expiration ?? "MM/YY";
                }
            }
            catch
            {
                // Puedes manejar errores aquí
            }
        }

        private string MaskCardNumber(string cardNumber)
        {
            if (cardNumber.Length < 4)
                return "****-****-****-****";

            var lastFour = cardNumber.Substring(cardNumber.Length - 4);
            return $"****-****-****-{lastFour}";
        }
    }
}
