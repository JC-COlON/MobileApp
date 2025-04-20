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
        // Abre el popup de "Seleccionar Método de Pago"
        var popup = new PoputSeleccionarMetodoPago(); // Crea una nueva instancia del popup
        Application.Current.MainPage.ShowPopupAsync(popup); // Muestra el popup
    }


    private async void Volveratras(object sender, TappedEventArgs e)
    {
        // Vuelve a la página anterior
        await Navigation.PopAsync(); // Asume que se está usando navegación basada en `NavigationPage`
    }


}