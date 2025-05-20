using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewModel;
using Newtonsoft.Json;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputSeleccionarMetodoPago : Popup
{
    private PagarMultaCiudadanoViewModel _mainViewModel;

    public PoputSeleccionarMetodoPago(PagarMultaCiudadanoViewModel mainViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;
        BindingContext = new ListadoTarjetaCiudadanoViewModel();
       
    }


    private void CerrarPopup(object sender, EventArgs e)
    {
        Close(); // Cierra el popup
    }

    private async void OnCardClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var param = button.CommandParameter;

            // Verificar si el parámetro es del tipo correcto
            if (param is DigesettAPP.ViewModel.CreditCard tarjeta)
            {
                // Mostrar un alert de confirmación
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Confirmación",
                    $"¿Seguro que quiere elegir esta tarjeta?\nTerminación: **** {tarjeta.CardNumber.ToString().Substring(tarjeta.CardNumber.ToString().Length - 4)}\nExpira: {tarjeta.ExpirationDate}",
                    "Sí", "No");

                if (confirm)
                {
                    // Si el usuario confirma, actualizar el ViewModel con la tarjeta seleccionada
                    _mainViewModel.NumeroTarjeta = $"**** **** **** {tarjeta.CardNumber.ToString().Substring(tarjeta.CardNumber.ToString().Length - 4)}";
                    _mainViewModel.Expiracion = $" {tarjeta.ExpirationDate}";
                    _mainViewModel.CardId = tarjeta.CardId;
                    _mainViewModel.ColorNumeroTarjeta = Colors.Black;
                    _mainViewModel.PuedePagar = true;

                    Close(); // Cerrar el popup o la vista actual
                }
                else
                {
                    // Si el usuario no confirma, no hacer nada
                    return;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No se pudo obtener la tarjeta correctamente", "OK");
            }
        }
    }















    private async void AgregarNuevaTarjeta_Tapped(object sender, EventArgs e)
    {
        Close(); // Cierra el popup antes de navegar
        await Shell.Current.GoToAsync(nameof(AgregarTarjetaCiudadano));
    }

}


