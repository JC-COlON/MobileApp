namespace DigesettAPP.Views;

public partial class CambiarLaContrasena : ContentPage
{
    private string token;

    // Aseg�rate de que tu p�gina acepte un token en el constructor
    public CambiarLaContrasena(string token)
    {
        InitializeComponent();
        this.token = token;

        // Ahora puedes usar el token para restablecer la contrase�a o hacer validaciones adicionales
    }
}
