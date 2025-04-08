using CommunityToolkit.Maui.Views;

namespace DigesettAPP.Views;


public partial class PoputCambiarContrasena : Popup

{
    public PoputCambiarContrasena()
	{
		InitializeComponent();
	}


    // Este m�todo se ejecuta cuando el usuario hace clic en el bot�n "Aceptar"
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        // Cerrar el pop-up
        this.Close();

        // Despu�s de cerrar, redirigir a la p�gina Home que tiene el TabBar
        await Shell.Current.GoToAsync("///Perfil");
    }
}