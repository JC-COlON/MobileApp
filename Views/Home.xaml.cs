namespace DigesettAPP.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

    private async void GotoCrearMultas_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Paso1Page());
    }

    private async void GoCrearUsuarioPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearUsuarioPage());
    }
    
}