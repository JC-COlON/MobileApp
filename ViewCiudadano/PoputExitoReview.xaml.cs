using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputExitoReview : Popup
{
	public PoputExitoReview()
	{
		InitializeComponent();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await this.CloseAsync(); // Solo cerrar el popup
    }


}