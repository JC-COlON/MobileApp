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
        // Obtener el Ticket desde el BindingContext de la imagen
        var image = sender as Image;
        if (image?.BindingContext is Ticket ticket)
        {
            await DisplayAlert("Pago", $"Vas a pagar la multa #{ticket.TicketId}", "OK");
        }
    }
}