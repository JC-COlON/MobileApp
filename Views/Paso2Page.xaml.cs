using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;
using DigesettAPP.Models;
using System.Text.Json;
using System.Text.Json.Serialization; // Asegúrate de que este espacio de nombres esté presente para la clase 'Article'

namespace DigesettAPP.Views
{
    public partial class Paso2Page : ContentPage
    {
        private Multa _multa;
        private Article articuloSeleccionado; // Variable para almacenar el artículo seleccionado

        public Paso2Page(Multa multa)
        {
            InitializeComponent();
            _multa = multa;

            // Definir las opciones del Picker para tipo de vehículo (el Picker para tipo de vehículo aún es necesario)
            tipoVehiculoPicker.ItemsSource = new List<string>
            {
                "Motocicleta",
                "Automóvil",
                "Camioneta",
                "Autobús",
                "Otros"
            };

            // Cargar los datos guardados, si existen
            CargarDatosGuardadosPaso2();
        }

        private void GuardarDatosPaso2()
        {
            // Guardar el artículo seleccionado en lugar del ID del Picker
            Preferences.Set("ArticuloInfringido", articuloSeleccionado?.DisplayText); // Almacena el texto de DisplayText del artículo seleccionado
            Preferences.Set("TipoVehiculo", tipoVehiculoPicker.SelectedItem?.ToString());

            // Guardar otros campos
            Preferences.Set("LugarIncidente", lugarIncidenteEntry.Text);
            Preferences.Set("PlacaVehiculo", placaVehiculoEntry.Text);
            Preferences.Set("ModeloVehiculo", modeloVehiculoEntry.Text);
            Preferences.Set("MarcaVehiculo", marcaVehiculoEntry.Text);
            Preferences.Set("Observaciones", ObservacionesEntry.Text);
        }

        private void CargarDatosGuardadosPaso2()
        {
            // Cargar el texto guardado en lugar del ID
            lugarIncidenteEntry.Text = Preferences.Get("LugarIncidente", string.Empty);

            // Recuperar el artículo infringido guardado y mostrar el texto correspondiente
            var articuloInfringidoText = Preferences.Get("ArticuloInfringido", string.Empty);
            if (!string.IsNullOrEmpty(articuloInfringidoText))
            {
                articuloSeleccionadoLabel.Text = articuloInfringidoText;
                articuloSeleccionadoLabel.TextColor = Colors.Black; // Cambia el color si ya está seleccionado
            }

            placaVehiculoEntry.Text = Preferences.Get("PlacaVehiculo", string.Empty);
            modeloVehiculoEntry.Text = Preferences.Get("ModeloVehiculo", string.Empty);
            marcaVehiculoEntry.Text = Preferences.Get("MarcaVehiculo", string.Empty);

            // Recuperar el texto del tipo de vehículo y seleccionarlo
            var tipoVehiculoText = Preferences.Get("TipoVehiculo", string.Empty);
            tipoVehiculoPicker.SelectedItem = tipoVehiculoText;

            ObservacionesEntry.Text = Preferences.Get("Observaciones", string.Empty);
        }

        // Método de validación
        private bool ValidarFormulario()
        {
            // Comprobar los campos obligatorios
            if (string.IsNullOrEmpty(lugarIncidenteEntry.Text))
            {
                DisplayAlert("Error", "'Zona / Lugar del Incidente' obligatorio", "OK");
                return false;
            }
            else if (articuloSeleccionado == null) // Validación de que se haya seleccionado un artículo
            {
                DisplayAlert("Error", "'Artículo Infringido' obligatorio", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(placaVehiculoEntry.Text))
            {
                DisplayAlert("Error", "'Placa del Vehículo' obligatorio", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(marcaVehiculoEntry.Text))
            {
                DisplayAlert("Error", "'Marca del Vehículo' obligatorio", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(tipoVehiculoPicker.SelectedItem?.ToString()))
            {
                DisplayAlert("Error", "'Tipo de Vehículo' obligatorio", "OK");
                return false;
            }

            return true;
        }

        private async void IrAtras2(object sender, EventArgs e)
        {
            // Validar el formulario antes de retroceder
            if (ValidarFormulario())
            {
                // Guardar los datos antes de retroceder
                GuardarDatosPaso2();
                await Navigation.PopAsync(); // ← Regresa a la página anterior
            }
        }


        private async void IrPaso3(object sender, EventArgs e)
        {
            // Validar el formulario antes de continuar al paso 3
            if (ValidarFormulario())
            {
                // Guardar los datos antes de continuar al paso 3
                GuardarDatosPaso2();
                await Navigation.PushAsync(new Paso3Page());
            }
        }

        // Método para abrir el popup de selección de artículo
        private async void AbrirPopupArticulo(object sender, EventArgs e)
        {
            var popup = new PoputSeleccionarArticulo();
            var result = await this.ShowPopupAsync(popup); // Espera el resultado

            if (result is Article selectedArticle)
            {
                articuloSeleccionado = selectedArticle; // Asigna el artículo seleccionado
                articuloSeleccionadoLabel.Text = selectedArticle.DisplayText; // Muestra el texto del artículo en el Label
                articuloSeleccionadoLabel.TextColor = Colors.Black; // Cambia el color a negro si se selecciona un artículo

                // Guardar el texto y el ID del artículo seleccionado en Preferences
                Preferences.Set("ArticuloInfringido", selectedArticle.DisplayText);  // Guardar el texto para mostrarlo
                Preferences.Set("ArticuloInfringidoId", selectedArticle.articleId.ToString()); // Guardar el ID para enviarlo
            }
        }

        private async void BuscarVehiculoPorPlaca(object sender, FocusEventArgs e)
        {
            var placa = placaVehiculoEntry.Text?.Trim();

            if (string.IsNullOrEmpty(placa))
                return;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://1037-200-215-234-53.ngrok-free.app/api/VehicleInfo/SearchByLicensePlate/{placa}";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var resultado = JsonSerializer.Deserialize<VehicleResponse>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        var vehiculo = resultado?.Vehicles?.Values?.FirstOrDefault();

                        if (vehiculo != null)
                        {
                            marcaVehiculoEntry.Text = vehiculo.Brand;
                            modeloVehiculoEntry.Text = vehiculo.Model;
                            // Puedes usar también vehiculo.Color, vehiculo.Year, etc.
                        }
                        else
                        {
                            bool registrar = await App.Current.MainPage.DisplayAlert(
                                "Vehículo no encontrado",
                                "¿Desea registrar este vehículo?",
                                "Sí", "No");

                            if (registrar)
                            {
                                // Aquí pasamos la placa al constructor del popup
                                var popup = new PopupAgregarVehiculo(placa);
                                await App.Current.MainPage.ShowPopupAsync(popup);
                            }
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        bool registrar = await App.Current.MainPage.DisplayAlert(
                            "Vehículo no encontrado",
                            "¿Desea registrar este vehículo?",
                            "Sí", "No");

                        if (registrar)
                        {
                            // Aquí pasamos la placa al constructor del popup
                            var popup = new PopupAgregarVehiculo(placa);
                            await App.Current.MainPage.ShowPopupAsync(popup);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo obtener la información del vehículo.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al buscar la placa: {ex.Message}", "OK");
            }
        }








    }
}

public class VehicleResponse
{
    public VehiclesWrapper Vehicles { get; set; }
}

public class VehiclesWrapper
{
    [JsonPropertyName("$values")]
    public List<Vehicle> Values { get; set; }
}

public class Vehicle
{
    public int VehicleInfoId { get; set; }
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
}


