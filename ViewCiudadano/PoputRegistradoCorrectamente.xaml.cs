using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputRegistradoCorrectamente : Popup
{
	public PoputRegistradoCorrectamente()
	{
		InitializeComponent();
	}


    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        // Cerrar el pop-up
        this.Close();

        // Después de cerrar, redirigir a la página LoginPage
        await Shell.Current.GoToAsync("//LoginPage");
    }

}