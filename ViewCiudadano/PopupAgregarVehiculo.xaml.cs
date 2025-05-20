using CommunityToolkit.Maui.Views;
using System.Net.Http.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using DigesettAPP.Models; // Asegúrate de que aquí esté tu clase Vehicle, o crea una local

namespace DigesettAPP.ViewCiudadano
{
    public partial class PopupAgregarVehiculo : Popup
    {
        public string Placa { get; set; }

        public PopupAgregarVehiculo(string placa = "")
        {
            InitializeComponent();
            Placa = placa;
            PlacaEntry.Text = Placa;  // Asigna la placa al Entry
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(PlacaEntry.Text))
            {
                await App.Current.MainPage.DisplayAlert("Campo requerido", "La placa es obligatoria.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(MarcaEntry.Text))
            {
                await App.Current.MainPage.DisplayAlert("Campo requerido", "La marca es obligatoria.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ModeloEntry.Text))
            {
                await App.Current.MainPage.DisplayAlert("Campo requerido", "El modelo es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ColorEntry.Text))
            {
                await App.Current.MainPage.DisplayAlert("Campo requerido", "El color es obligatorio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(YearEntry.Text))
            {
                await App.Current.MainPage.DisplayAlert("Campo requerido", "El año es obligatorio.", "OK");
                return;
            }

            if (!int.TryParse(YearEntry.Text, out int parsedYear))
            {
                await App.Current.MainPage.DisplayAlert("Campo inválido", "El año debe ser un número válido.", "OK");
                return;
            }

            var nuevoVehiculo = new
            {
                vehicleInfoId = 0,
                licensePlate = PlacaEntry.Text.Trim(),
                brand = MarcaEntry.Text.Trim(),
                model = ModeloEntry.Text.Trim(),
                color = ColorEntry.Text.Trim(),
                year = parsedYear
            };

            try
            {
                using var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(120)
                };

                var url = "https://digesett.somee.com/api/VehicleInfo/AddVehicle";
                var response = await client.PostAsJsonAsync(url, nuevoVehiculo);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Éxito", "Vehículo agregado correctamente.", "OK");

                    // Devuelve el vehículo como resultado del popup
                    var vehiculoAgregado = new Vehicle
                    {
                        LicensePlate = PlacaEntry.Text.Trim(),
                        Brand = MarcaEntry.Text.Trim(),
                        Model = ModeloEntry.Text.Trim(),
                        Color = ColorEntry.Text.Trim(),
                        Year = parsedYear
                    };

                    Close(vehiculoAgregado); // ← Esto envía los datos al cerrar
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
            Close(); // Cierra sin retornar nada
        }
    }
}
