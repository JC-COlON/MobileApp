using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;

namespace DigesettAPP.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}

    private async void GotoCrearMultas_Clicked(object sender, EventArgs e)
    {
        // Obtener el botón que fue presionado
        var imageButton = (ImageButton)sender;

        // Obtener el contenedor padre (Frame) del botón
        var parentFrame = imageButton.Parent?.Parent as Frame;

        if (parentFrame != null)
        {
            // Animación combinada de escala y opacidad
            await Task.WhenAll(
                parentFrame.ScaleTo(0.95, 100, Easing.CubicOut),
                parentFrame.FadeTo(0.8, 100)
            );

            // Volver a la normalidad
            await Task.WhenAll(
                parentFrame.ScaleTo(1, 100, Easing.CubicIn),
                parentFrame.FadeTo(1, 100)
            );
        }

        // Navegación
        await Shell.Current.GoToAsync(nameof(Paso1Page));
    }



    //private async void GoCrearUsuarioPage(object sender, EventArgs e)
    //{
    //    // Obtener el botón que fue presionado
    //    var imageButton = (ImageButton)sender;

    //    // Obtener el contenedor padre (Frame) del botón
    //    var parentFrame = imageButton.Parent?.Parent as Frame;

    //    if (parentFrame != null)
    //    {
    //        // Animación combinada de escala y opacidad
    //        await Task.WhenAll(
    //            parentFrame.ScaleTo(0.95, 100, Easing.CubicOut),
    //            parentFrame.FadeTo(0.8, 100)
    //        );

    //        // Volver a la normalidad
    //        await Task.WhenAll(
    //            parentFrame.ScaleTo(1, 100, Easing.CubicIn),
    //            parentFrame.FadeTo(1, 100)
    //        );
    //    }

    //    // Navegación
    //    await Shell.Current.GoToAsync(nameof(CrearUsuarioPage));
    //}





    private async void GoBuscarMultaPage(object sender, EventArgs e)
    {
        // Obtener el botón que fue presionado
        var imageButton = (ImageButton)sender;

        // Obtener el contenedor padre (Frame) del botón
        var parentFrame = imageButton.Parent?.Parent as Frame;

        if (parentFrame != null)
        {
            // Animación combinada de escala y opacidad
            await Task.WhenAll(
                parentFrame.ScaleTo(0.95, 100, Easing.CubicOut),
                parentFrame.FadeTo(0.8, 100)
            );

            // Volver a la normalidad
            await Task.WhenAll(
                parentFrame.ScaleTo(1, 100, Easing.CubicIn),
                parentFrame.FadeTo(1, 100)
            );
        }

        // Navegación
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