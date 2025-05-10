using CommunityToolkit.Maui.Views;
using DigesettAPP.Views;
using System.IdentityModel.Tokens.Jwt;


namespace DigesettAPP.ViewCiudadano;

public partial class PerfilPageCiudadano : ContentPage
{
    public PerfilPageCiudadano()
    {
        InitializeComponent();
        MostrarInformacionUsuario();

        // Suscribirse al mensaje "PerfilActualizado"
        MessagingCenter.Subscribe<object>(this, "PerfilActualizado", (sender) => {
            MostrarInformacionUsuario();
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Desuscribirse del mensaje para evitar fugas de memoria
        MessagingCenter.Unsubscribe<object>(this, "PerfilActualizado");
    }





    protected override void OnAppearing()
    {
        base.OnAppearing();
        MostrarInformacionUsuario(); // ⚠️ Recargar la información cada vez que la página se muestra
    }

    // Método para obtener la información del usuario desde el token JWT
    private void MostrarInformacionUsuario()
    {
        LoadingOverlay.IsVisible = true;

        try
        {
            var nombreCompleto = ObtenerNombreCompletoDesdeToken();
            var cedula = ObtenerCedulaDesdeToken();
            var noAgente = Preferences.Get("NoAgente", "No disponible");
            var email = ObtenerEmailDesdeToken();
            var telefono = ObtenerTelefonoDesdeToken();

            UsuarioNombreLabel.Text = nombreCompleto;
            CedulaLabel.Text = cedula;
            emaillabel.Text = email;
            telefonolabel.Text = telefono;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"No se pudo cargar la información del perfil: {ex.Message}", "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }


    // Método para obtener el nombre completo del usuario desde el token JWT
    private string ObtenerNombreCompletoDesdeToken()
    {
        var nombre = ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
        var apellido = ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname");
        return $"{nombre} {apellido}";
    }



    // Método para obtener la cédula desde el token JWT
    private string ObtenerCedulaDesdeToken()
    {
        return ObtenerClaimDesdeToken("http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");
    }

    private string ObtenerEmailDesdeToken()
    {
        return ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
    }

    private string ObtenerTelefonoDesdeToken()
    {
        return ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone");
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

    private async void IrASobreApp(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SobreApp());
    }

    private async void IrEditar(object sender, EventArgs e)
    {
        var result = await App.Current.MainPage.ShowPopupAsync(new PopupEditarCiudadano());

        if (result is true) // ✅ Si se devolvió true, recargar info
        {
            MostrarInformacionUsuario(); // 🔁 Recarga los datos en la vista de perfil
        }
    }




    private async void IrAChangePassword(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChangePassword());
    }

    private async void SalirDeLaApp(object sender, EventArgs e)
    {
        bool confirm = await Application.Current.MainPage.DisplayAlert(
            "Confirmación",
            "¿Estás seguro de que deseas cerrar sesión?",
            "Sí",
            "No"
        );

        if (confirm)
        {
            // Limpiar todas las preferencias almacenadas
            Preferences.Clear();

            // ⚠️ Asegurar que el usuario actual en memoria también se borre
            App.user = null;

            // ⚠️ Forzar una recarga de la aplicación o navegación para evitar datos en caché
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}