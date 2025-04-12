using CommunityToolkit.Maui.Views;

namespace DigesettAPP.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

    private async void GotoCrearMultas_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Paso1Page));
    }

    private async void GoCrearUsuarioPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CrearUsuarioPage));
    }

    private async void GoBuscarMultaPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BuscarMultaPage));
    }



}