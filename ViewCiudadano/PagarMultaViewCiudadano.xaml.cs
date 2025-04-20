using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PagarMultaViewCiudadano : ContentPage
{
	public PagarMultaViewCiudadano()
	{
		InitializeComponent();
	}

    private void ElegirNuevaTarjeta_Tapped(object sender, TappedEventArgs e)
    {
        // Abre el popup de "Seleccionar M�todo de Pago"
        var popup = new PoputSeleccionarMetodoPago(); // Crea una nueva instancia del popup
        Application.Current.MainPage.ShowPopupAsync(popup); // Muestra el popup
    }


    private async void Volveratras(object sender, TappedEventArgs e)
    {
        // Vuelve a la p�gina anterior
        await Navigation.PopAsync(); // Asume que se est� usando navegaci�n basada en `NavigationPage`
    }


}