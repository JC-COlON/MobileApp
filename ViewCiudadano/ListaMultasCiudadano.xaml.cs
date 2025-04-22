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
            this.ShowPopup(popup); // <-- Requiere usar CommunityToolkit.Maui.Views
        }

        private async void OnMoneyIconTapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            if (image?.BindingContext is Ticket ticket)
            {


                // Llevamos el ticketId directamente al constructor
                var page = new PagarMultaViewCiudadano(ticket.TicketId);
                await Navigation.PushAsync(page);
            }
        }


    }
}
