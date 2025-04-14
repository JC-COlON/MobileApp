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


    private async void AbrirNoticia_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton imageButton && imageButton.CommandParameter is string url)
        {
            try
            {
                await Launcher.Default.OpenAsync(url);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo abrir la noticia: {ex.Message}", "OK");
            }
        }
    }



}