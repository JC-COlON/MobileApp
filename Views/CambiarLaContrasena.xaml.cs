namespace DigesettAPP.Views;

public partial class CambiarLaContrasena : ContentPage
{
    private string token;

    // Asegúrate de que tu página acepte un token en el constructor
    public CambiarLaContrasena(string token)
    {
        InitializeComponent();
        this.token = token;

        // Ahora puedes usar el token para restablecer la contraseña o hacer validaciones adicionales
    }
}
