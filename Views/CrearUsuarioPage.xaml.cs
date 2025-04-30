using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Net.Http.Headers;
using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;

namespace DigesettAPP.Views
{
    public partial class CrearUsuarioPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
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


        public CrearUsuarioPage(string cedula)
        {
            InitializeComponent();

            CedulaEntry.Text = cedula;

            // Validación automática si tiene 11 dígitos
            if (!string.IsNullOrEmpty(cedula) && cedula.Length == 11)
            {
                _ = ValidarCedula(cedula); // Ejecutar validación en segundo plano
            }

            NombreEntry.IsEnabled = false;
            ApellidoEntry.IsEnabled = false;
            TelefonoEntry.IsEnabled = false;
            CrearUsuarioButton.IsEnabled = false;
        }


        // Evento TextChanged para validar la cédula automáticamente cuando tenga 11 dígitos
        private async void CedulaEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cedula = CedulaEntry.Text?.Trim();

            // Verifica si el texto tiene 11 dígitos
            if (cedula.Length == 11)
            {
                await ValidarCedula(cedula);
            }
        }

        // Método para validar la cédula
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error validando cédula: {ex.Message}");
                await DisplayAlert("Error", "Ocurrió un error validando la cédula. Intente de nuevo.", "OK");
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
            await DisplayAlert("JSON Enviado", jsonPayload, "OK");

            try
            {
                await MainThread.InvokeOnMainThreadAsync(() => IsLoading = true);

                using var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(120)
                };

                var content = new StringContent(
                    jsonPayload,
                    Encoding.UTF8,
                    "application/json"
                );

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(Url, content);
                await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.ShowPopupAsync(new PopupCreadoExito());

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
                    await DisplayAlert("Error", $"No se pudo crear el usuario. Respuesta: {responseContent}", "OK");
                }
            }
            catch (HttpRequestException ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);
                await DisplayAlert("Error de conexión", $"No se pudo conectar al servidor: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() => IsLoading = false);
                await DisplayAlert("Error inesperado", $"Ocurrió un error: {ex.Message}", "OK");
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
