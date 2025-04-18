using Microsoft.Maui.Controls;
using DigesettAPP.ViewModels;

namespace DigesettAPP.Views
{
    public partial class ListaMultasCiudadano : ContentPage
    {
        public ListaMultasCiudadano()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as ListaMultaViewModel;
            viewModel?.CargarMultasCommand.Execute(null);  // Ejecutamos el comando al aparecer la página
        }

        private async void OnInfoTapped(object sender, EventArgs e)
        {
            //// Reemplaza "InfoPage" con el nombre real de tu página
            //await Navigation.PushAsync(new InfoPage());
        }
    }
}
