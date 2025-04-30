using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PopupCreadoExito : Popup
{
    public PopupCreadoExito()
    {
        InitializeComponent();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        this.Close(); // Cierra el popup

        await Task.Delay(100); // Espera un poco para asegurar que el popup se cerró

        // Cierra CrearUsuario y vuelve a Paso1
        await Shell.Current.Navigation.PopAsync(); // Vuelve a Paso1
    }


}