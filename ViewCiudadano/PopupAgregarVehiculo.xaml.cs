using CommunityToolkit.Maui.Views;
using System.Net.Http.Json;

namespace DigesettAPP.ViewCiudadano
{
    public partial class PopupAgregarVehiculo : Popup
    {
        public string Placa { get; set; }

        public PopupAgregarVehiculo(string placa = "")
        {
            InitializeComponent();
            Placa = placa;
            PlacaEntry.Text = Placa;  // Aquí asignamos la placa al Entry en el popup
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validaciones una por una
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
                    Timeout = TimeSpan.FromSeconds(120) // ⬅️ Aquí le agregas también el Timeout de 2 minutos
                };
                var url = "https://digesett.somee.com/api/VehicleInfo/AddVehicle";
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
}
