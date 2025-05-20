using DigesettAPP.ViewModel;

namespace DigesettAPP.ViewCiudadano;

public partial class HistorialMultasPagadasCiudadano : ContentPage
{
	public   HistorialMultasPagadasCiudadano()
	{
		InitializeComponent();
        BindingContext = new HistorialMultasPagasViewModel();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as HistorialMultasPagasViewModel;
        viewModel?.CargarMultasCommand.Execute(null);  // Ejecutamos el comando al aparecer la página
    }

 

}