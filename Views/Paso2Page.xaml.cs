using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewCiudadano;
using DigesettAPP.Models; // Asegúrate de que este espacio de nombres esté presente para la clase 'Article'

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

    }
}
