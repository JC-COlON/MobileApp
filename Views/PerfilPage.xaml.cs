using System.IdentityModel.Tokens.Jwt;
using Microsoft.Maui.Storage; // Para trabajar con Preferences

namespace DigesettAPP.Views
{
    public partial class PerfilPage : ContentPage
    {
        public PerfilPage()
        {
            InitializeComponent();
            MostrarInformacionUsuario();
        }

        // Método para obtener la información del usuario desde el token JWT
        private void MostrarInformacionUsuario()
        {
            var nombreCompleto = ObtenerNombreCompletoDesdeToken();
            var ciudad = ObtenerCiudadDesdeToken();
            var cedula = ObtenerCedulaDesdeToken();

            // Asignar la información al Label correspondiente
            UsuarioNombreLabel.Text = nombreCompleto;
            CiudadLabel.Text = ciudad;
            CedulaLabel.Text = cedula;
        }

        // Método para obtener el nombre completo del usuario desde el token JWT
        private string ObtenerNombreCompletoDesdeToken()
        {
            var nombre = ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            var apellido = ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname");

            return $"{nombre} {apellido}";
        }

        // Método para obtener la ciudad desde el token JWT
        private string ObtenerCiudadDesdeToken()
        {
            return ObtenerClaimDesdeToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality");
        }

        // Método para obtener la cédula desde el token JWT
        private string ObtenerCedulaDesdeToken()
        {
            return ObtenerClaimDesdeToken("http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");
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

        private async void SalirDeLaApp(object sender, EventArgs e)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirmación",
                "¿Estás seguro de que deseas cerrar sesión?",
                "Sí",
                "No"
            );

            if (confirm) // Si el usuario elige "Sí"
            {
                Preferences.Clear(); // Elimina los datos almacenados

                // Redirigir al login asegurando que los campos se limpien
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}
