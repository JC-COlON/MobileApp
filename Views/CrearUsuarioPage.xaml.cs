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
                password = "",  // Debe enviarse como cadena vacía
                rolId = 3,
                email = "",  // Debe enviarse como cadena vacía
                phone = TelefonoEntry.Text,
                profileImg = "",  // Debe enviarse como cadena vacía
                nacionalityId = (int?)null,
                birthdate = (DateTime?)null,
                genderId = (int?)null,
                bloodTypeId = (int?)null,
                height = (double?)null,
                workLocationId = (int?)null,
                civilStatusId = (int?)null,
                noAgente = "",  // Debe enviarse como cadena vacía
                statusId = 1  // Debe ser 1 siempre
            };

            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(nuevoUsuario,
                    new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(Url, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Usuario creado correctamente.", "OK");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"No se pudo crear el usuario: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
            }
        }

    }
}
