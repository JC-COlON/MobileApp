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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzgyMTc3OEAzMjM5MmUzMDJlMzAzYjMyMzkzYld1RmdmVFF2eXl0cmM5L08zN0RkMHlTTDRkaEpHRVFhdGV1bDR3U2h5N2s9");
            MainPage = new AppShell();
            Preferences.Clear();


            Shell.Current.GoToAsync(nameof(AgregarTarjetaCiudadano));

            //Shell.Current.GoToAsync($"//{nameof(AgregarTarjetaCiudadano)}");


            //MainPage.Dispatcher.Dispatch(async () =>
            //{
            //    var popup = new PopupAgregarVehiculo();
            //    await MainPage.ShowPopupAsync(popup);
            //});


            // Navegar a la página de "HomeViewCiudadano" al iniciar la app

            //MainPage.Dispatcher.Dispatch(async () =>
            //{
            //    // Navegar a la página de inicio directamente
            //    await Shell.Current.GoToAsync("//HomeViewCiudadano");
            //});



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
