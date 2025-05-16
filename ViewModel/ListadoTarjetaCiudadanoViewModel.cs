using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Windows.Input;
using DigesettAPP.Models;
using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;

namespace DigesettAPP.ViewModel
{
    public class ListadoTarjetaCiudadanoViewModel : BindableObject
    {
        // URL base de ngrok
        private const string BaseUrl = "https://5fce-200-215-234-53.ngrok-free.app/api/CreditCard";

        // Cambié la propiedad Tarjetas para que sea directamente una lista de CreditCard
        public ObservableCollection<CreditCard> Tarjetas { get; set; } = new ObservableCollection<CreditCard>();

        private bool _alertaMostrada = false;

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

        public ICommand CargarTarjetasCommand { get; }
        public ICommand EliminarTarjetaCommand { get; }

        public ListadoTarjetaCiudadanoViewModel()
        {
            CargarTarjetasCommand = new Command(async () => await CargarTarjetas());
            EliminarTarjetaCommand = new Command<CreditCard>(async (card) => await EliminarTarjeta(card));
            _ = CargarTarjetas(); // carga al iniciar
        }






        private async Task CargarTarjetas()
        {
            if (_alertaMostrada) return;

            string cedula = ObtenerCedulaDelToken();
            if (string.IsNullOrEmpty(cedula))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            try
            {
                IsLoading = true;

                using var client = new HttpClient();
                string token = Preferences.Get("AuthToken", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{BaseUrl}/GetCreditCardsByCedula?cedula={cedula}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    if (!_alertaMostrada)
                    {
                        _alertaMostrada = true;

                        var respuesta = await App.Current.MainPage.DisplayAlert(
                            "No tiene tarjetas registradas",
                            "¿Desea registrar una tarjeta?",
                            "Sí",
                            "No"
                        );

                        if (respuesta)
                        {
                            await Shell.Current.GoToAsync(nameof(AgregarTarjetaCiudadano));
                        }
                        else
                        {
                            await Shell.Current.GoToAsync("..");
                        }
                    }

                    return; // Terminar aquí para evitar cualquier otro mensaje
                }

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var tarjetas = JsonConvert.DeserializeObject<List<CreditCard>>(json);
                    Tarjetas = new ObservableCollection<CreditCard>(tarjetas ?? new List<CreditCard>());
                    OnPropertyChanged(nameof(Tarjetas));
                }

                else
                {
                    // ⚠️ Ya no mostramos alerta en caso de error si no es 404
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsLoading = false;
            }
        }



        private async Task EliminarTarjeta(CreditCard card)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmar", "¿Estás seguro que deseas eliminar esta tarjeta?", "Sí", "No");
            if (!confirm) return;

            try
            {
                IsLoading = true;

                using var client = new HttpClient();
                string token = Preferences.Get("AuthToken", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"{BaseUrl}/{card.CardId}");
                if (response.IsSuccessStatusCode)
                {
                    Tarjetas.Remove(card); // Elimina la tarjeta de la lista
                    await App.Current.MainPage.ShowPopupAsync(new PoputTarjetaEliminadaExito());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar la tarjeta.", "OK");
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
                var token = Preferences.Get("AuthToken", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                var cedula = jwtToken?.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber")?.Value;

                return cedula;
            }
            catch
            {
                return null;
            }
        }
    }

    public class CreditCard
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public long CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public string CardNumberMasked => $"****-****-****-{CardNumber.ToString().Substring(CardNumber.ToString().Length - 4)}";

        public string ExpirationDateInfo => $"Exp: {ExpirationDate}";

    }
}

