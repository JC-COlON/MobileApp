using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewModel;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputSeleccionarMetodoPago : Popup
{
    public PoputSeleccionarMetodoPago()
    {
        InitializeComponent();

        BindingContext = new ListadoTarjetaCiudadanoViewModel(); // Asignas aquí el ViewModel
    }

    private void CerrarPopup(object sender, EventArgs e)
    {
        Close(); // Cierra el popup
    }

    private async void OnCardTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is CreditCard tarjeta)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Tarjeta Seleccionada",
                $"CardId: {tarjeta.CardId}\nCardNumber: {tarjeta.CardNumber}\nExpira: {tarjeta.ExpirationDate}",
                "OK"
            );
        }
    }

    private async void AgregarNuevaTarjeta_Tapped(object sender, EventArgs e)
    {
        Close(); // Cierra el popup antes de navegar
        await Shell.Current.GoToAsync(nameof(AgregarTarjetaCiudadano));
    }

}


