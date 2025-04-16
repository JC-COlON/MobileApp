namespace DigesettAPP.ViewCiudadano;

public partial class HomeViewCiudadano : ContentPage
{
	public HomeViewCiudadano()
	{
		InitializeComponent();
	}


    private async void IrPerfilCiudadano(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PerfilPageCiudadano());
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var frame2 = (Frame)sender;

        // Animación combinada de opacidad y escala
        await Task.WhenAll(
            frame2.ScaleTo(0.95, 100, Easing.CubicOut),
            frame2.FadeTo(0.8, 100) // Reducir la opacidad al hacer clic
        );

        // Volver a la normalidad
        await Task.WhenAll(
            frame2.ScaleTo(1, 100, Easing.CubicIn),
            frame2.FadeTo(1, 100) // Restaurar la opacidad
        );

        await Navigation.PushAsync(new AgregarTarjetaCiudadano());

    }



}