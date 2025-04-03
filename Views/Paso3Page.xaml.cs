namespace DigesettAPP.Views;

public partial class Paso3Page : ContentPage
{
	public Paso3Page()
	{
		InitializeComponent();
	}

    private async void IrAtras(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}