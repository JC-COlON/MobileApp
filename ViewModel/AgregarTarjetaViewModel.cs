using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Threading.Tasks;

namespace DigesettAPP.ViewModels
{
    public partial class AgregarTarjetaViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        string cardNumber;

        [ObservableProperty]
        string expirationDate;

        [ObservableProperty]
        string cvv;

        // Propiedades formateadas para mostrar en la tarjeta
        public string FormattedCardNumber => FormatCardNumber(CardNumber);
        public string FormattedExpirationDate => FormatExpirationDate(ExpirationDate);
        public string FormattedCVV => string.IsNullOrWhiteSpace(Cvv) ? "***" : Cvv;

        public AgregarTarjetaViewModel()
        {
            _httpClient = new HttpClient();
            RegisterCardCommand = new RelayCommand(async () => await RegisterCard());
        }

        public ICommand RegisterCardCommand { get; }

        // Formatear número de tarjeta al escribir
        partial void OnCardNumberChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            var digits = new string(value.Where(char.IsDigit).ToArray());

            var formatted = string.Join("-", Enumerable.Range(0, digits.Length / 4 + (digits.Length % 4 == 0 ? 0 : 1))
                .Select(i => digits.Skip(i * 4).Take(4))
                .Where(g => g.Any())
                .Select(g => string.Concat(g)));

            if (formatted != CardNumber)
                CardNumber = formatted;

            OnPropertyChanged(nameof(FormattedCardNumber));
        }

        // Formatear fecha de expiración al escribir
        partial void OnExpirationDateChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            var digits = new string(value.Where(char.IsDigit).ToArray());

            if (digits.Length >= 3)
                digits = digits.Insert(2, "/");

            if (digits.Length > 5)
                digits = digits.Substring(0, 5);

            if (digits != ExpirationDate)
                ExpirationDate = digits;

            OnPropertyChanged(nameof(FormattedExpirationDate));
        }

        partial void OnCvvChanged(string value)
        {
            OnPropertyChanged(nameof(FormattedCVV));
        }

        private string FormatCardNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "0000 - 0000 - 0000 - 0000";

            var digits = new string(input.Where(char.IsDigit).ToArray());
            digits = digits.PadRight(16, '0');

            return $"{digits.Substring(0, 4)} - {digits.Substring(4, 4)} - {digits.Substring(8, 4)} - {digits.Substring(12, 4)}";
        }

        private string FormatExpirationDate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "MM/AA";

            var digits = new string(input.Where(char.IsDigit).ToArray());
            if (digits.Length >= 4)
                return digits.Insert(2, "/").Substring(0, 5);

            return digits;
        }

        private async Task RegisterCard()
        {
            try
            {
                var token = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrEmpty(token))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Token no encontrado", "OK");
                    return;
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener el userId del token", "OK");
                    return;
                }

                int userId = int.Parse(userIdClaim.Value);

                var cardInfo = new
                {
                    userId = userId,
                    cardNumber = new string(CardNumber?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>()),
                    expirationDate = new string(ExpirationDate?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>()),
                    cvv = Cvv
                };

                var json = JsonConvert.SerializeObject(cardInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7277/api/CreditCard", content);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Éxito", "Tarjeta registrada correctamente", "OK");
                }
                else
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var errorObj = JObject.Parse(errorJson);
                        var mensaje = errorObj["mensaje"]?.ToString() ?? "Error desconocido";
                        await App.Current.MainPage.DisplayAlert("Error", mensaje, "OK");
                    }
                    catch
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudo procesar la respuesta del servidor", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
}
