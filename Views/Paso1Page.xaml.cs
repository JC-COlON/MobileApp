namespace DigesettAPP.Views;

public partial class Paso1Page : ContentPage
{
	public Paso1Page()
	{
		InitializeComponent();
	}


    private async void IrPaso2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Paso2Page());
    }

    private async void IrAtras(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}