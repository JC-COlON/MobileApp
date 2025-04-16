using CommunityToolkit.Maui.Views;
using DigesettAPP.Models;
using DigesettAPP.ViewCiudadano;
using DigesettAPP.Views;
using Microsoft.Maui.Storage;

namespace DigesettAPP
{
    public partial class App : Application
    {
        public static User user;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            Shell.Current.GoToAsync(nameof(RegistrarViewCiudadano));
        }

        // Este método se llama cuando la app se pone en segundo plano (o está por cerrarse)
        protected override void OnSleep()
        {
            base.OnSleep();
            LimpiarDatosTemporales();
        }

        private void LimpiarDatosTemporales()
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
}
