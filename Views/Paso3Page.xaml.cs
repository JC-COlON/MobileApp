using CommunityToolkit.Maui.Views;
using DigesettAPP.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DigesettAPP.Views;

public partial class Paso3Page : ContentPage
{
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Verifica si la página está disponible antes de mostrar la información
        if (this.IsVisible)
        {
            MostrarInformacionUsuario(); // ⚠️ Recargar la información cada vez que la página se muestra
        }
    }

    public Paso3Page()
    {
        InitializeComponent();

        // Cargar los datos de las preferencias y asignarlos a los Labels
        zoneLabel.Text = Preferences.Get("LugarIncidente", "No especificado");
        firstNameLabel.Text = Preferences.Get("Nombre", "No especificado");
        lastNameLabel.Text = Preferences.Get("Apellido", "No especificado");
        phoneLabel.Text = Preferences.Get("Telefono", "No especificado");
        emailLabel.Text = Preferences.Get("Email", "No especificado");
        identificationLabel.Text = Preferences.Get("Cedula", "No especificado");
        vehicleTypeLabel.Text = Preferences.Get("TipoVehiculo", "No especificado");
        vehiclePlateLabel.Text = Preferences.Get("PlacaVehiculo", "No especificado");
        brandLabel.Text = Preferences.Get("MarcaVehiculo", "No especificado");
        modelLabel.Text = Preferences.Get("ModeloVehiculo", "No especificado");
        infringedArticleLabel.Text = Preferences.Get("ArticuloInfringido", "No especificado");
        observationsLabel.Text = Preferences.Get("Observaciones", "No especificado");
        agentNumberLabel.Text = Preferences.Get("NoAgente", "No especificado");

   
    }


    private string ObtenerUserIdDesdeToken()
    {
        // Obtener el claim que corresponde al userId (en este caso, nameidentifier)
        string userIdAgente = ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        // 🔴 Depuración: Mostrar en consola el valor extraído
        Console.WriteLine($"📌 userId extraído del token: {userIdAgente}");

        return userIdAgente;
    }

    private void MostrarInformacionUsuario()
    {
       
        var userIdAgente = ObtenerUserIdDesdeToken(); // Obtener el userId del token
        Preferences.Set("UserIdAgente", userIdAgente); // Guardar el userId en las preferencias




    }


    // Método general para obtener cualquier claim desde el token
    private string ObtenerClaimDesdeToken(string claimType)
    {
        // Recuperar el token JWT almacenado
        string token = Preferences.Get("AuthToken", string.Empty);

        if (string.IsNullOrEmpty(token))
            return "Información no disponible";

        // Decodificar el token
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        // Extraer el claim del token
        var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return claimValue ?? "Información no disponible";
    }

    private void IrAtras(object sender, EventArgs e)
    {
        // Asegúrate de que la página no esté siendo descartada antes de intentar hacer alguna operación
        if (this.IsVisible)
        {
            // Realiza la acción de navegación
            Navigation.PopAsync();
        }
    }


    private async void EnviarMulta(object sender, EventArgs e)
    {
        LoadingOverlay.IsVisible = true;

        try
        {
            // Obtener los datos de las preferencias
            int userId = int.TryParse(Preferences.Get("UserId", "0"), out var uid) ? uid : 0;
            string zone = Preferences.Get("LugarIncidente", "");
            string firstName = Preferences.Get("Nombre", "");
            string lastName = Preferences.Get("Apellido", "");
            string phone = Preferences.Get("Telefono", "");
            string email = Preferences.Get("Email", "");
            string identification = Preferences.Get("Cedula", "");
            string vehicleType = Preferences.Get("TipoVehiculo", "");
            string vehiclePlate = Preferences.Get("PlacaVehiculo", "");
            string brand = Preferences.Get("MarcaVehiculo", "");
            string model = Preferences.Get("ModeloVehiculo", "");
            string observations = Preferences.Get("Observaciones", "");
            string agentNumber = Preferences.Get("NoAgente", "");

            int agentId = int.TryParse(Preferences.Get("UserIdAgente", "0"), out var aid) ? aid : 0;
            int tipoVehiculoId = DataPicker.tipoVehiculoMap.ContainsKey(vehicleType) ? DataPicker.tipoVehiculoMap[vehicleType] : -1;
            int articuloInfringidoId = int.TryParse(Preferences.Get("ArticuloInfringidoId", "-1"), out var articleId) ? articleId : -1;

            bool respuesta = await DisplayAlert("Confirmación de Envío", "¿Estás seguro de que deseas enviar esta multa?", "Enviar", "Cancelar");

            if (respuesta)
            {
                var multa = new
                {
                    userId = userId,
                    zone = zone,
                    firstName = firstName,
                    lastName = lastName,
                    phone = phone,
                    email = email,
                    identification = identification,
                    vehicleTypeId = tipoVehiculoId,
                    vehiclePlate = vehiclePlate,
                    brand = brand,
                    model = model,
                    infringedArticle = articuloInfringidoId,
                    incidentLocation = zone,
                    observations = observations,
                    photoUrl = "",
                    agentNumber = agentNumber,
                    status = "",
                    agentId = agentId
                };

                var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(180)
                };

                var jsonContent = JsonConvert.SerializeObject(multa);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                int retries = 3;
                HttpResponseMessage response = null;

                for (int attempt = 1; attempt <= retries; attempt++)
                {
                    try
                    {
                        response = await client.PostAsync("https://digesett.somee.com/api/Ticket/CreateMultaEnviarEmail", content);

                        if (response.IsSuccessStatusCode)
                        {
                            LimpiarDatos();
                            var popup = new PopupPage();
                            await Application.Current.MainPage.ShowPopupAsync(popup);
                            return; // Salir completamente después del éxito
                        }
                        else
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            if (attempt == retries)
                            {
                                await DisplayAlert("Error", $"Hubo un error al registrar la multa: {errorMessage}", "Aceptar");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (attempt == retries)
                        {
                            await DisplayAlert("Error", $"No se pudo conectar con el servidor. Error: {ex.Message}", "Aceptar");
                        }

                        await Task.Delay(1000 * attempt); // Espera progresiva
                    }
                }
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }




    private void LimpiarDatos()
    {
        Preferences.Remove("Cedula");
        Preferences.Remove("Nombre");
        Preferences.Remove("Apellido");
        Preferences.Remove("Telefono");
        Preferences.Remove("Email");

        Preferences.Remove("LugarIncidente");
        Preferences.Remove("ArticuloInfringido");
        Preferences.Remove("PlacaVehiculo");
        Preferences.Remove("ModeloVehiculo");
        Preferences.Remove("MarcaVehiculo");
        Preferences.Remove("TipoVehiculo");
        Preferences.Remove("Observaciones");
    }



}
