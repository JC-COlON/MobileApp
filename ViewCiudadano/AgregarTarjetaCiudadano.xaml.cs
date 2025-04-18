using DigesettAPP.ViewModels;

namespace DigesettAPP.ViewCiudadano;

public partial class AgregarTarjetaCiudadano : ContentPage
{
	public AgregarTarjetaCiudadano()
	{
		InitializeComponent();
        BindingContext = new AgregarTarjetaViewModel();
    }
}