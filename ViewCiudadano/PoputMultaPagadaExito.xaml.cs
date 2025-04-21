using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputMultaPagadaExito : Popup
{
	public PoputMultaPagadaExito()
	{
		InitializeComponent();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        // Cerrar el pop-up
        this.Close();

        // Después de cerrar, redirigir a la página Home que tiene el TabBar
        await Shell.Current.GoToAsync("ListaMultasParaPagar");
    }
}