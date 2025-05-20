using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using DigesettAPP.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Maui.Controls;

namespace DigesettAPP.ViewModel
{
    internal class HomeCiudadanoViewModel : BindableObject
    {
        private const string BaseUrl = "https://digesett.somee.com/api/Ticket/Pending";

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

        private bool _hayMultas;
        public bool HayMultas
        {
            get => _hayMultas;
            set
            {
                _hayMultas = value;
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

        public HomeCiudadanoViewModel()
        {
            CargarMultasPendientes();
        }

        private async void CargarMultasPendientes()
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
                        var multas = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);

                        if (multas != null && multas.Count > 0)
                        {
                            ListaDeMultas = new ObservableCollection<Ticket>(multas);
                            HayMultas = true;
                        }
                        else
                        {
                            HayMultas = false;
                        }
                    }
                    else
                    {
                        HayMultas = false;
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudieron cargar las multas.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al cargar multas: {ex.Message}", "OK");
                HayMultas = false;
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
                if (string.IsNullOrEmpty(token))
                    return null;

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                var cedulaClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "nameidentifier" || c.Type.EndsWith("/nameidentifier"));
                return cedulaClaim?.Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
