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
        this.Close();

        // Limpia la pila de navegaci�n anterior usando Shell y ruta absoluta temporal
        await Shell.Current.GoToAsync("//HomeViewCiudadano");

        // Luego navega a la p�gina deseada
        await Shell.Current.GoToAsync(nameof(ListaMultasParaPagar));
    }

}