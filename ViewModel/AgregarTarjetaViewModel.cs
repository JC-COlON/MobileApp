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
using System.Text.RegularExpressions;

namespace DigesettAPP.ViewModels
{
    public partial class AgregarTarjetaViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        string carName;

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
        partial void OnCardNumberChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            // Filtra solo los dígitos
            var digits = new string(value.Where(char.IsDigit).ToArray());

            // Limita a 16 dígitos máximo
            if (digits.Length > 16)
                digits = digits.Substring(0, 16);

            // Aplica el formato con guiones cada 4
            var formatted = string.Join("-", Enumerable.Range(0, digits.Length / 4 + (digits.Length % 4 == 0 ? 0 : 1))
                .Select(i => digits.Skip(i * 4).Take(4))
                .Where(g => g.Any())
                .Select(g => string.Concat(g)));

            // Solo actualiza si cambia
            if (formatted != CardNumber)
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    CardNumber = formatted;
                });
            }

            OnPropertyChanged(nameof(FormattedCardNumber));
        }



        partial void OnExpirationDateChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            // Filtra solo los dígitos
            var digits = new string(value.Where(char.IsDigit).ToArray());

            // Formato de fecha de expiración: MM/AA
            if (digits.Length >= 3)
                digits = digits.Insert(2, "/");

            // Limita el tamaño del campo (solo 5 caracteres como máximo: MM/AA)
            if (digits.Length > 5)
                digits = digits.Substring(0, 5);

            // Solo actualiza si el valor cambia
            if (digits != ExpirationDate)
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    ExpirationDate = digits;
                });
            }

            OnPropertyChanged(nameof(FormattedExpirationDate));
        }


        partial void OnCvvChanged(string value)
        {
            OnPropertyChanged(nameof(FormattedCVV));
        }

        private string FormatCardNumber(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "0000-0000-0000-0000" : input;
        }

        private string FormatExpirationDate(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "MM/AA" : input;
        }

        private async Task RegisterCard()
        {
            int maxRetries = 3;  // Número máximo de reintentos
            int delayBetweenRetries = 2000;  // Retraso en milisegundos (2 segundos)

            for (int retryCount = 0; retryCount < maxRetries; retryCount++)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(CardNumber) ||
                        string.IsNullOrWhiteSpace(ExpirationDate) ||
                        string.IsNullOrWhiteSpace(Cvv))
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                        return;
                    }

                    var cardNumberDigits = new string(CardNumber.Where(char.IsDigit).ToArray());

                    if (cardNumberDigits.Length != 16)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "El número de la tarjeta debe tener 16 dígitos.", "OK");
                        return;
                    }

                    if (!long.TryParse(cardNumberDigits, out long cardNumberLong))
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Número de tarjeta inválido.", "OK");
                        return;
                    }

                    if (Cvv.Length != 3 || !Cvv.All(char.IsDigit))
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "El CVV debe tener 3 dígitos numéricos.", "OK");
                        return;
                    }

                    if (!Regex.IsMatch(ExpirationDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "La fecha de expiración debe estar en formato MM/AA.", "OK");
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

                    var cardInfo = new
                    {
                        userId = userId,
                        cardNumber = cardNumberLong,
                        expirationDate = ExpirationDate,
                        cvv = Cvv
                    };

                    var json = JsonConvert.SerializeObject(cardInfo, Formatting.Indented);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Intentar realizar la solicitud hasta el número máximo de reintentos
                    var response = await _httpClient.PostAsync("https://5fce-200-215-234-53.ngrok-free.app/api/CreditCard", content);

                    if (response.IsSuccessStatusCode)
                    {
                        await App.Current.MainPage.ShowPopupAsync(new PoputTarjetaAgregada());

                        // Limpiar campos
                        CardNumber = string.Empty;
                        ExpirationDate = string.Empty;
                        Cvv = string.Empty;
                        CarName = string.Empty;
                        return;
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
                    if (retryCount == maxRetries - 1)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
                    }
                    else
                    {
                        // Si no es el último intento, esperar un tiempo antes de reintentar
                        await Task.Delay(delayBetweenRetries);
                    }
                }
            }
        }

    }
}
