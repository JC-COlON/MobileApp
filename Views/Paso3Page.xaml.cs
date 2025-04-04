using System.IdentityModel.Tokens.Jwt;

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
        agentNumberLabel.Text = Preferences.Get("NoAgente", "No disponible");

        // 🚀 NUEVOS CAMPOS: Mostrar AgentId y UserId
        userIdLabelAGENTE.Text = Preferences.Get("UserIdAgente", "No disponible");

        userIdLabel.Text = Preferences.Get("UserId", "No disponible");
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

}
