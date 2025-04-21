using DigesettAPP.ViewModels;

namespace DigesettAPP.ViewCiudadano;

public partial class ListaMultasParaPagar : ContentPage
{
	public ListaMultasParaPagar()
	{
		InitializeComponent();
        BindingContext = new ListaMultasParaPagarViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as ListaMultasParaPagarViewModel;
        if (viewModel?.Multas == null || viewModel.Multas.Count == 0)
            viewModel?.CargarMultasCommand.Execute(null);
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