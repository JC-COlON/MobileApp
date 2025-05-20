using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputMultaPagadaExito : Popup
{
	public PoputMultaPagadaExito()
	{
		InitializeComponent();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        this.Close();

        await Shell.Current.GoToAsync("../..");

    }

}