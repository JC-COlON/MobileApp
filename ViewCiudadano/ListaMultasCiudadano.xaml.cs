using Microsoft.Maui.Controls;
using DigesettAPP.ViewModels;
using DigesettAPP.ViewCiudadano;
using CommunityToolkit.Maui.Views;

namespace DigesettAPP.Views
{
    public partial class ListaMultasCiudadano : ContentPage
    {
        public ListaMultasCiudadano()
        {
            InitializeComponent();
            BindingContext = new ListaMultaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as ListaMultaViewModel;
            viewModel?.CargarMultasCommand.Execute(null);  // Ejecutamos el comando al aparecer la página
        }

        private async void OnInfoTapped(object sender, EventArgs e)
        {
            var popup = new PoputInformacionRating();
            this.ShowPopup(popup); 
        }

        


    }
}
