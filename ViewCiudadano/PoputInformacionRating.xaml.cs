using CommunityToolkit.Maui.Views;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputInformacionRating : Popup
{
	public PoputInformacionRating()
	{
		InitializeComponent();
	}


    private void ClosePopup(object sender, EventArgs e)
    {
        this.Close(); // Cierra el popup
    }
}