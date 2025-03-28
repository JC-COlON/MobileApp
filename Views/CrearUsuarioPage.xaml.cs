using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigesettAPP.Views
{
    public partial class CrearUsuarioPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string Url = "https://digesett.somee.com/api/UserAccess/Create";

        public CrearUsuarioPage()
        {
            InitializeComponent();
        }

        private async void OnCrearUsuarioClicked(object sender, EventArgs e)
        {
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

            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(nuevoUsuario,
                    new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(Url, content);

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
                    Console.WriteLine($"Error al crear usuario. Código: {response.StatusCode}");
                    Console.WriteLine($"Mensaje del servidor: {responseContent}");

                    await DisplayAlert("Error", $"No se pudo crear el usuario.\nCódigo: {response.StatusCode}\nMensaje: {responseContent}", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción atrapada: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
            }
        }
    }
}
