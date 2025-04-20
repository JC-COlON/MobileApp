using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputTarjetaEliminadaExito : Popup
{
	public PoputTarjetaEliminadaExito()
	{
		InitializeComponent();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        // Cerrar el pop-up
        this.Close();

        // Después de cerrar, redirigir a la página Home que tiene el TabBar
        await Shell.Current.GoToAsync("ListadoTarjetasCiudadano");
    }
}