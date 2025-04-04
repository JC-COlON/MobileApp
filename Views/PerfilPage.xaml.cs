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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MostrarInformacionUsuario(); // ⚠️ Recargar la información cada vez que la página se muestra
        }

        // Método para obtener la información del usuario desde el token JWT
        private void MostrarInformacionUsuario()
        {
            var nombreCompleto = ObtenerNombreCompletoDesdeToken();
            var ciudad = ObtenerCiudadDesdeToken();
            var cedula = ObtenerCedulaDesdeToken();
            var noAgente = Preferences.Get("NoAgente", "No disponible");




            // Asignar la información a los Labels
            UsuarioNombreLabel.Text = nombreCompleto;
            CiudadLabel.Text = ciudad;
            CedulaLabel.Text = cedula;
            NoAgenteLabel.Text = $"Agente No: {noAgente}";  // ✅ Mostrar NoAgente
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
}
