using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PopupActualizadoCorrectamente : Popup
{
    public PopupActualizadoCorrectamente()
    {
        InitializeComponent();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await this.CloseAsync(true); // Devuelve true al cerrarse
    }
}
