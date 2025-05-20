using CommunityToolkit.Maui.Views;
using DigesettAPP.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PopupCreadoExito : Popup
{
    private readonly string _cedula;

    // Modificar el constructor para recibir la c�dula como par�metro
    public PopupCreadoExito(string cedula)
    {
        InitializeComponent();
        _cedula = cedula;  // Asignar la c�dula pasada al campo _cedula
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        this.Close(); // Cierra el popup
        await Task.Delay(100); // Espera para asegurar el cierre

        // Enviar c�dula al Paso1Page
        MessagingCenter.Send(this, "UsuarioCreado", _cedula);

        // Buscar Paso1Page en la pila de navegaci�n y volver a ella
        var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;
        var paso1Page = navigationStack.FirstOrDefault(p => p is Paso1Page);

        if (paso1Page != null)
        {
            while (Application.Current.MainPage.Navigation.NavigationStack.Last() != paso1Page)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        else
        {
            // Si no est� en la pila, navega a una nueva instancia
            await Application.Current.MainPage.Navigation.PushAsync(new Paso1Page());
        }
    }

}
