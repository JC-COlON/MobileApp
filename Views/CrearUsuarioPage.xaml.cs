using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DigesettAPP.Views
{
    public partial class CrearUsuarioPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string Url = "https://localhost:7277/api/User/Create";
        private const string CedulaValidationUrl = "https://api.digital.gob.do/v3/cedulas";

        public CrearUsuarioPage()
        {
            InitializeComponent();

            // Deshabilita los campos al inicio
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
            // Validación de campos (sin cambios)
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
                password = "",                     // Cambiado a cadena vacía
                rolId = 3,
                email = "",                         // Cambiado a cadena vacía
                phone = TelefonoEntry.Text,
                profileImg = "",                    // Cambiado a cadena vacía
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

            try
            {
                // Serialización para mostrar (puede usar cualquier método)
                string jsonBonito = Newtonsoft.Json.JsonConvert.SerializeObject(nuevoUsuario, Newtonsoft.Json.Formatting.Indented);
                await DisplayAlert("JSON enviado", jsonBonito, "OK");

                // Serialización para enviar (¡esto es lo importante!)
                string json = System.Text.Json.JsonSerializer.Serialize(nuevoUsuario);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(Url, content);

                // Resto del código sin cambios...
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Usuario creado correctamente.", "OK");

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
                    try
                    {
                        var apiError = JsonConvert.DeserializeObject<ApiError>(responseContent);

                        if (apiError?.Errors != null && apiError.Errors.Count > 0)
                        {
                            var erroresFormateados = apiError.Errors
                                .Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}")
                                .ToList();

                            string mensajeError = string.Join("\n", erroresFormateados);

                            await DisplayAlert("Errores de validación", mensajeError, "OK");
                        }
                        else
                        {
                            await DisplayAlert("Error del servidor", apiError?.Title ?? "Ocurrió un error desconocido.", "OK");
                        }
                    }
                    catch
                    {
                        await DisplayAlert("Error", $"No se pudo crear el usuario.\nMensaje del servidor:\n{responseContent}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción atrapada: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
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
