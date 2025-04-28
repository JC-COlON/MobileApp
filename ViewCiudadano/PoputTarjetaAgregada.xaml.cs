using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputTarjetaAgregada : Popup
{
	public PoputTarjetaAgregada()
	{
		InitializeComponent();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        // Cerrar el pop-up
        this.Close();

        await Shell.Current.GoToAsync("..");
    }
}