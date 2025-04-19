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
using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;

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
            // Simplemente retorna los dígitos sin formatear
            return string.IsNullOrWhiteSpace(input) ? "0000000000000000" : input;
        }

        private string FormatExpirationDate(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "MM/AA" : input;
        }

        private async Task RegisterCard()
        {
            try
            {
                // Verificar que todos los campos estén completos
                if (string.IsNullOrWhiteSpace(CardNumber) ||
                    string.IsNullOrWhiteSpace(ExpirationDate) ||
                    string.IsNullOrWhiteSpace(cvv))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                    return;
                }

                // Limpiar el número de la tarjeta de cualquier carácter no numérico
                var cardNumberDigits = new string(CardNumber.Where(char.IsDigit).ToArray());

                // Verificar que el número de la tarjeta tiene 16 dígitos
                if (cardNumberDigits.Length != 16)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El número de la tarjeta debe tener 16 dígitos.", "OK");
                    return;
                }

                // Convertir el número de la tarjeta a long (BigInt)
                if (!long.TryParse(cardNumberDigits, out long cardNumberLong))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Número de tarjeta inválido.", "OK");
                    return;
                }

                // Validar que el CVV tenga exactamente 3 dígitos numéricos
                if (cvv.Length != 3 || !cvv.All(char.IsDigit))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El CVV debe tener 3 dígitos numéricos.", "OK");
                    return;
                }

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

                // Asegúrate de formatear la fecha de expiración antes de enviarla
                var formattedExpirationDate = FormatExpirationDate(ExpirationDate);

                var cardInfo = new
                {
                    userId = userId,
                    cardNumber = cardNumberLong,
                    expirationDate = formattedExpirationDate,
                    cvv = cvv
                };

                var json = JsonConvert.SerializeObject(cardInfo, Formatting.Indented);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7277/api/CreditCard", content);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.ShowPopupAsync(new PoputTarjetaAgregada());
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
                    catch (JsonException jsonEx)
                    {
                        await App.Current.MainPage.DisplayAlert("Error de JSON", $"No se pudo procesar la respuesta del servidor. Error JSON: {jsonEx.Message}", "OK");
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", $"Error al procesar la respuesta: {ex.Message}", "OK");
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