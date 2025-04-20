using DigesettAPP.ViewModel;

namespace DigesettAPP.ViewCiudadano
{
    public partial class ListadoTarjetasCiudadano : ContentPage
    {
        public ListadoTarjetasCiudadano()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as ListadoTarjetaCiudadanoViewModel;
            viewModel?.CargarTarjetasCommand.Execute(null); // Ejecuta el comando para cargar las tarjetas
        }

        private async void OnAgregarTarjetaClicked(object sender, EventArgs e)
        {
            // Redirigir a la vista para agregar tarjeta
            await Shell.Current.GoToAsync(nameof(AgregarTarjetaCiudadano));

        }

    }
}
