namespace DigesettAPP.Views
{
    public partial class Paso2Page : ContentPage
    {
        private Multa _multa;

        public Paso2Page(Multa multa)
        {
            InitializeComponent();
            _multa = multa;

            // Definir las opciones del Picker para tipo de veh�culo
            tipoVehiculoPicker.ItemsSource = new List<string>
            {
                "Motocicleta",
                "Autom�vil",
                "Camioneta",
                "Autob�s",
                "Otros"
            };

            // Definir las opciones del Picker para art�culo infringido
            articuloInfringidoPicker.ItemsSource = new List<string>
            {
                "ART 29 : Sin licencia",
                "ART 40-41 : Licencia vencida",
                "ART 47-7 : No portar licencia",
                "ART 146/02 : Sin marbete de seguro",
                "ART 27-3 : Sin matr�cula",
                "ART 27-4 : Sin placa",
                "ART 110 : Sin revista",
                "ART 1 LEY513 : Sin tablilla",
                "ART 135C : Sin casco",
                "ART 33-77 : Pirata no rotul�",
                "ART 88 : Obstrucci�n al tr�nsito",
                "ART 81 : Sitio (zona) prohibido",
                "ART 97 : Se�ales de tr�nsito",
                "ART 97-A : Violar se�al de pare",
                "ART 76-C : Viraje en U",
                "ART 27-22 : Una placa",
                "ART 27-2 : Uso distinto de matr�cula",
                "ART 27-27 : Color distinto de matr�cula",
                "ART 174 : Personas sobre la carga",
                "ART 162 : Ruido",
                "ART 74-H : Ceder paso a veh�culo de emergencia",
                "ART 27-4 : No exhibir la placa en su lugar",
                "ART 144 : Luz delantera apagada",
                "ART 145 : Luz trasera",
                "ART 158 : Espejo retrovisor",
                "ART 164-B : Extinguidor",
                "ART 164 AP 200 : Botiqu�n",
                "ART 171 A-3 : Bandera roja",
                "ART 173-G : Plataforma y timbre",
                "ART 173-D : Pe�n",
                "ART 164-D : Tri�ngulo",
                "ART 130 LEY 241 : Ventas v�as p�blicas",
                "ART 130 : Lanzar desperdicios en v�as p�blicas",
                "ART 62 : Velocidad muy reducida",
                "ART 5 : Gu�a a la izquierda",
                "ART 27 Y 47 : Actos prohibidos",
                "ART 161 : Sin cintur�n",
                "ART 96-B-1 : Luz roja",
                "ART 1 LEY 143-01 : Uso del celular",
                "ART 106 : Ni�os menores en el asiento delantero"
            };

            // Cargar los datos guardados, si existen
            CargarDatosGuardadosPaso2();
        }

        private void GuardarDatosPaso2()
        {
            // Guardar el texto seleccionado en lugar del ID
            Preferences.Set("ArticuloInfringido", articuloInfringidoPicker.SelectedItem?.ToString());
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

            // Recuperar el texto del Picker desde las preferencias y seleccionar el �tem correspondiente
            var articuloInfringidoText = Preferences.Get("ArticuloInfringido", string.Empty);
            articuloInfringidoPicker.SelectedItem = articuloInfringidoText;

            placaVehiculoEntry.Text = Preferences.Get("PlacaVehiculo", string.Empty);
            modeloVehiculoEntry.Text = Preferences.Get("ModeloVehiculo", string.Empty);
            marcaVehiculoEntry.Text = Preferences.Get("MarcaVehiculo", string.Empty);

            // Recuperar el texto del tipo de veh�culo y seleccionarlo
            var tipoVehiculoText = Preferences.Get("TipoVehiculo", string.Empty);
            tipoVehiculoPicker.SelectedItem = tipoVehiculoText;

            ObservacionesEntry.Text = Preferences.Get("Observaciones", string.Empty);
        }

        // M�todo de validaci�n
        private bool ValidarFormulario()
        {
            // Comprobar los campos obligatorios
            if (string.IsNullOrEmpty(lugarIncidenteEntry.Text))
            {
                DisplayAlert("Error", "'Zona / Lugar del Incidente' obligatorio", "OK");
                return false;
            }
            else if (articuloInfringidoPicker.SelectedIndex == -1)
            {
                DisplayAlert("Error", "'Art�culo Infringido' obligatorio", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(placaVehiculoEntry.Text))
            {
                DisplayAlert("Error", "'Placa del Veh�culo' obligatorio", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(marcaVehiculoEntry.Text))
            {
                DisplayAlert("Error", "'Marca del Veh�culo' obligatorio", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(tipoVehiculoPicker.SelectedItem?.ToString()))
            {
                DisplayAlert("Error", "'Tipo de Veh�culo' obligatorio", "OK");
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
                await Navigation.PopAsync();
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
    }
}
