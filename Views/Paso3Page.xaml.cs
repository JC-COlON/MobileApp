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

        // 🚀 NUEVOS CAMPOS: Mostrar AgentId y UserId
        //userIdLabelAGENTE.Text = Preferences.Get("UserIdAgente", "No disponible");

        //userIdLabel.Text = Preferences.Get("UserId", "No disponible");
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

        // Verificación en consola (o en un punto de depuración)
        Console.WriteLine($"🚀 userIdAgente guardado en Preferences: {userIdAgente}");


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
        // Obtener los datos de las preferencias
        var userId = Preferences.Get("UserId", "No disponible");
        var zone = Preferences.Get("LugarIncidente", "No especificado");
        var firstName = Preferences.Get("Nombre", "No especificado");
        var lastName = Preferences.Get("Apellido", "No especificado");
        var phone = Preferences.Get("Telefono", "No especificado");
        var email = Preferences.Get("Email", "No especificado");
        var identification = Preferences.Get("Cedula", "No especificado");
        var vehicleType = Preferences.Get("TipoVehiculo", "No especificado");
        var vehiclePlate = Preferences.Get("PlacaVehiculo", "No especificado");
        var brand = Preferences.Get("MarcaVehiculo", "No especificado");
        var model = Preferences.Get("ModeloVehiculo", "No especificado");
        var infringedArticle = Preferences.Get("ArticuloInfringido", "No especificado");
        var observations = Preferences.Get("Observaciones", "No especificado");
        var agentNumber = Preferences.Get("NoAgente", "No disponible");

        // Obtener el userId del agente (de un claim del token)
        var userIdAgente = Preferences.Get("UserIdAgente", "No disponible");

        // Convertir el tipo de vehículo a ID (si está guardado como texto)
        var tipoVehiculoId = DataPicker.tipoVehiculoMap.ContainsKey(vehicleType) ? DataPicker.tipoVehiculoMap[vehicleType] : -1;

        // Convertir el artículo infringido a ID (si está guardado como texto)
        var articuloInfringidoId = DataPicker.articuloInfringidoMap.ContainsKey(infringedArticle) ? DataPicker.articuloInfringidoMap[infringedArticle] : -1;

        // Construir el mensaje para el alerta, mostrando solo los valores con ID
        var mensaje = $"ID del Usuario: {userId}\n" +
                      $"Zona del Incidente: {zone}\n" +
                      $"Nombre: {firstName}\n" +
                      $"Apellido: {lastName}\n" +
                      $"Teléfono: {phone}\n" +
                      $"Email: {email}\n" +
                      $"Cédula: {identification}\n" +
                      $"Tipo de Vehículo (ID): {tipoVehiculoId}\n" +
                      $"Placa del Vehículo: {vehiclePlate}\n" +
                      $"Marca del Vehículo: {brand}\n" +
                      $"Modelo del Vehículo: {model}\n" +
                      $"Artículo Infringido (ID): {articuloInfringidoId}\n" +
                      $"Observaciones: {observations}\n" +
                      $"Número de Agente: {agentNumber}\n" +
                      $"ID del Agente: {userIdAgente}\n\n" +
                      $"¿Estás seguro de que deseas enviar esta multa?";

        // Mostrar el mensaje en un alerta de confirmación
        bool respuesta = await DisplayAlert(
            "Confirmación de Envío",
            mensaje,
            "Enviar",
            "Cancelar");

        // Si el usuario hace clic en "Enviar", proceder con el envío
        if (respuesta)
        {
            // Construir el objeto Multa que se enviará a la API
            var multa = new
            {
                userId = userId, // ID del usuario (agente o administrador)
                zone = zone, // Zona
                firstName = firstName,
                lastName = lastName,
                phone = phone,
                email = email,
                identification = identification,
                vehicleTypeId = tipoVehiculoId, // Solo el ID del tipo de vehículo
                vehiclePlate = vehiclePlate,
                brand = brand,
                model = model,
                infringedArticle = articuloInfringidoId, // Solo el ID del artículo infringido
                incidentLocation = zone, // Ubicación del incidente
                observations = observations,
                photoUrl = "", // Si tienes una foto para enviar, se puede incluir aquí
                agentNumber = agentNumber,
                status = "", // Aquí puedes agregar el estado de la multa si corresponde
                agentId = userIdAgente // El ID del agente (del claim)
            };

            // Enviar la solicitud POST a la API
            try
            {
                var client = new HttpClient();
                var jsonContent = JsonConvert.SerializeObject(multa);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://digesett.somee.com/api/Ticket/CreateMulta", content);

                if (response.IsSuccessStatusCode)
                {
                    
                   

                    // Limpiar los datos guardados en Preferences
                    Preferences.Remove("UserId");
                    Preferences.Remove("LugarIncidente");
                    Preferences.Remove("Nombre");
                    Preferences.Remove("Apellido");
                    Preferences.Remove("Telefono");
                    Preferences.Remove("Email");
                    Preferences.Remove("Cedula");
                    Preferences.Remove("TipoVehiculo");
                    Preferences.Remove("PlacaVehiculo");
                    Preferences.Remove("MarcaVehiculo");
                    Preferences.Remove("ModeloVehiculo");
                    Preferences.Remove("ArticuloInfringido");
                    Preferences.Remove("Observaciones");
                    Preferences.Remove("NoAgente");
                    Preferences.Remove("UserIdAgente");

                    // Mostrar el pop-up
                    var popup = new PopupPage();
                    await Application.Current.MainPage.ShowPopupAsync(popup);
                }
                else
                {
                    // Hubo un error al registrar la multa
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al registrar la multa: {errorMessage}");
                    await DisplayAlert("Error", $"Hubo un error al registrar la multa: {errorMessage}", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de que falle la conexión o la solicitud
                Console.WriteLine($"Error de conexión: {ex.Message}");
                await DisplayAlert("Error", $"No se pudo conectar con el servidor. Error: {ex.Message}", "Aceptar");
            }
        }
    }




}
