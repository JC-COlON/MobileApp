using Microsoft.Maui.Controls;
using DigesettAPP.ViewModels;
using DigesettAPP.ViewCiudadano;
using CommunityToolkit.Maui.Views;
using System.Collections.Generic;

namespace DigesettAPP.Views
{
    public partial class ListaMultasCiudadano : ContentPage, IQueryAttributable
    {
        private ListaMultaViewModel ViewModel => BindingContext as ListaMultaViewModel;

        public ListaMultasCiudadano()
        {
            InitializeComponent();
            BindingContext = new ListaMultaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Si no viene con parámetro, se carga aquí
            if (!seRecibioTicketId)
                ViewModel?.CargarMultasCommand.Execute(null);
        }

        private bool seRecibioTicketId = false;

        // Este método se ejecuta automáticamente si navegas con parámetros
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("ticketId", out var idObj) && int.TryParse(idObj?.ToString(), out int ticketId))
            {
                seRecibioTicketId = true;
                await ViewModel.InicializarDesdeParametro(ticketId);
            }
        }

        private async void OnInfoTapped(object sender, EventArgs e)
        {
            var popup = new PoputInformacionRating();
            this.ShowPopup(popup);
        }
    }
}
