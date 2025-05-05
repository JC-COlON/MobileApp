using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Net.Http.Headers;
using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;
using System.Net;

namespace DigesettAPP.Views
{
    public partial class CrearUsuarioPage : ContentPage
    {
        private const string Url = "https://digesett.somee.com/api/UserAccess/Create";
        private const string CedulaValidationUrl = "https://api.digital.gob.do/v3/cedulas";

        private bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                LoadingOverlay.IsVisible = value;
            }
        }

        private readonly HttpClient _httpClient;

        public CrearUsuarioPage(string cedula)
        {
            InitializeComponent();

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(120)
            };

            CedulaEntry.Text = cedula;

            if (!string.IsNullOrEmpty(cedula) && cedula.Length == 11)
            {
                _ = ValidarCedula(cedula); // Ejecutar validación en segundo plano
            }

            NombreEntry.IsEnabled = false;
            ApellidoEntry.IsEnabled = false;
            TelefonoEntry.IsEnabled = false;
            CrearUsuarioButton.IsEnabled = false;
        }

        private async void CedulaEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cedula = CedulaEntry.Text?.Trim();

            if (cedula.Length == 11)
            {
                await ValidarCedula(cedula);
            }
        }

        private async Task ValidarCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula) || cedula.Length != 11)
            {
                await DisplayAlert("Cédula inválida", "La cédula debe contener 11 dígitos.", "OK");
                DeshabilitarCampos();
                return;
            }

            try
            {
                string url = $"{CedulaValidationUrl}/{cedula}/validate";
                var response = await _httpClient.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ValidacionCedulaResponse>(content);

                if (result != null && result.Valid)
                {
                    HabilitarCampos();
                }
                else
                {
                    await DisplayAlert("Cédula no válida", "La cédula introducida no es válida.", "OK");
                    DeshabilitarCampos();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error validando cédula: {ex.Message}");
                await DisplayAlert("Error", "Ocurrió un error validando la cédula. Intente de nuevo.", "OK");
                DeshabilitarCampos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error desconocido validando cédula: {ex.Message}");
                await DisplayAlert("Error", "Ocurrió un error inesperado. Intente de nuevo.", "OK");
                DeshabilitarCampos();
            }
        }

        private void HabilitarCampos()
        {
            NombreEntry.IsEnabled = true;
            ApellidoEntry.IsEnabled = true;
            TelefonoEntry.IsEnabled = true;
            CrearUsuarioButton.IsEnabled = true;
        }

        private void DeshabilitarCampos()
        {
            NombreEntry.IsEnabled = false;
            ApellidoEntry.IsEnabled = false;
            TelefonoEntry.IsEnabled = false;
            CrearUsuarioButton.IsEnabled = false;
        }

        private async void OnCrearUsuarioClicked(object sender, EventArgs e)
        {
            if (IsLoading) return;

            if (string.IsNullOrWhiteSpace(CedulaEntry.Text) ||
                string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(ApellidoEntry.Text) ||
                string.IsNullOrWhiteSpace(TelefonoEntry.Text))
            {
                await DisplayAlert("Campos requeridos", "Por favor, complete todos los campos antes de continuar.", "OK");
                return;
            }

            var nuevoUsuario = new
            {
                cedula = CedulaEntry.Text,
                name = NombreEntry.Text,
                lastname = ApellidoEntry.Text,
                password = "",
                rolId = 3,
                email = "",
                phone = TelefonoEntry.Text,
                profileImg = "",
                nacionalityId = (int?)null,
                birthdate = (DateTime?)null,
                genderId = (int?)null,
                bloodTypeId = (int?)null,
                height = (double?)null,
                workLocationId = (int?)null,
                civilStatusId = (int?)null,
                noAgente = "",
                statusId = 1
            };

            string jsonPayload = JsonConvert.SerializeObject(nuevoUsuario, Formatting.Indented);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            await MainThread.InvokeOnMainThreadAsync(() => IsLoading = true);

            HttpResponseMessage response = null;
            int retries = 3;  // Aumentamos los intentos

            for (int attempt = 1; attempt <= retries; attempt++)
            {
                try
                {
                    response = await _httpClient.PostAsync(Url, content);
                    break; // éxito
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error en intento {attempt}: {ex.Message}");

                    if (attempt == retries)
                    {
                        await DisplayAlert("Error de conexión", "No se pudo conectar al servidor. Intente más tarde.", "OK");
                        await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);
                        return;
                    }

                    await Task.Delay(1000 * attempt); // Exponencial backoff: esperar más tiempo con cada intento
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Error WebException en intento {attempt}: {ex.Message}");

                    if (attempt == retries)
                    {
                        await DisplayAlert("Error de red", "La conexión fue restablecida de manera inesperada. Intente más tarde.", "OK");
                        await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);
                        return;
                    }

                    await Task.Delay(1000 * attempt); // Exponencial backoff
                }
            }

            await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);

            if (response != null && response.IsSuccessStatusCode)
            {
                // Aquí pasamos directamente `CedulaEntry.Text` al Popup
                await App.Current.MainPage.ShowPopupAsync(new PopupCreadoExito(CedulaEntry.Text));

                var usuarioCreado = new Usuario
                {
                    Cedula = CedulaEntry.Text,
                    Name = NombreEntry.Text,
                    LastName = ApellidoEntry.Text,
                    Email = "",
                    Phone = TelefonoEntry.Text
                };

                await Navigation.PopAsync();
                MessagingCenter.Send(this, "UsuarioCreado", usuarioCreado);
            }
            else
            {
                string responseContent = response != null ? await response.Content.ReadAsStringAsync() : "Sin respuesta";
                await DisplayAlert("Error", $"No se pudo crear el usuario. Respuesta: {responseContent}", "OK");
            }
        }





        // Clase para mapear el error de la respuesta
        public class ErrorResponse
        {
            [JsonProperty("error")]
            public string Error { get; set; }
        }

        // Clase para mapear la respuesta de la validación de cédula
        public class ValidacionCedulaResponse
        {
            [JsonProperty("valid")]
            public bool Valid { get; set; }
        }


        public class ApiError
        {
            public string Title { get; set; }
            public int Status { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
            public string TraceId { get; set; }
        }

    }
}
