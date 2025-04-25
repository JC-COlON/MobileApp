using CommunityToolkit.Maui.Views;
using System.Formats.Tar;
using System.Net.Http.Json;


namespace DigesettAPP.ViewCiudadano;
public partial class PopupAgregarVehiculo : Popup
{
    public string Placa { get; set; }

    public PopupAgregarVehiculo(string placa)
    {
        InitializeComponent();
        Placa = placa;
        PlacaEntry.Text = Placa;  // Aquí asignamos la placa al Entry en el popup
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var nuevoVehiculo = new
        {
            vehicleInfoId = 0,
            licensePlate = PlacaEntry.Text?.Trim(),
            brand = MarcaEntry.Text?.Trim(),
            model = ModeloEntry.Text?.Trim(),
            color = ColorEntry.Text?.Trim(),
            year = int.TryParse(YearEntry.Text, out int year) ? year : 0
        };

        try
        {
            using var client = new HttpClient();
            var url = "https://1037-200-215-234-53.ngrok-free.app/api/VehicleInfo/AddVehicle";
            var response = await client.PostAsJsonAsync(url, nuevoVehiculo);

            if (response.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Éxito", "Vehículo agregado correctamente.", "OK");
                Close();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el vehículo.", "OK");
            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

    private void OnCancelarClicked(object sender, EventArgs e)
    {
        Close();
    }
}
