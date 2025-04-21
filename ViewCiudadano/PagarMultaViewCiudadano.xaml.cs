using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewModel;
using Microsoft.Maui.Controls;

namespace DigesettAPP.ViewCiudadano
{
    public partial class PagarMultaViewCiudadano : ContentPage
    {
        private readonly PagarMultaCiudadanoViewModel _viewModel;

        public PagarMultaViewCiudadano(int ticketId)
        {
            InitializeComponent();
            _viewModel = new PagarMultaCiudadanoViewModel();
            BindingContext = _viewModel;

            // Llama a un método para cargar todos los datos
            CargarDatosIniciales(ticketId);
        }

        private async void CargarDatosIniciales(int ticketId)
        {
            await _viewModel.CargarDatos(); // Carga nombre, cédula, etc.
            await _viewModel.CargarMultaPorId(ticketId); // Carga multa específica
        }

        // Constructor vacío por si acaso
        public PagarMultaViewCiudadano()
        {
            InitializeComponent();
            _viewModel = new PagarMultaCiudadanoViewModel();
            BindingContext = _viewModel;
        }

        // Manejo del evento para elegir nueva tarjeta
        private void ElegirNuevaTarjeta_Tapped(object sender, TappedEventArgs e)
        {
            var popup = new PoputSeleccionarMetodoPago();
            Application.Current.MainPage.ShowPopupAsync(popup);
        }

        // Navegar hacia atrás
        private async void Volveratras(object sender, TappedEventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
