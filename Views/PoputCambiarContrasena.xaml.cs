using CommunityToolkit.Maui.Views;

namespace DigesettAPP.Views;


public partial class PoputCambiarContrasena : Popup

{
    public PoputCambiarContrasena()
	{
		InitializeComponent();
	}


    // Este método se ejecuta cuando el usuario hace clic en el botón "Aceptar"
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        // Cerrar el pop-up
        this.Close();

        // Después de cerrar, redirigir a la página Home que tiene el TabBar
        await Shell.Current.GoToAsync("///Perfil");
    }
}