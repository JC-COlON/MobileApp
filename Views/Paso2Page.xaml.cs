namespace DigesettAPP.Views;

public partial class Paso2Page : ContentPage
{
    public Paso2Page()
    {
        InitializeComponent();

        // Definir las opciones del Picker para tipo de vehículo
        tipoVehiculoPicker.ItemsSource = new List<string>
            {
                "Motocicleta",
                "Automóvil",
                "Camioneta",
                "Autobús",
                "Otros"
            };



        // Definir las opciones del Picker para artículo infringido
        articuloInfringidoPicker.ItemsSource = new List<string>
            {
            "ART 29 : Sin licencia",
            "ART 40-41 : Licencia vencida",
            "ART 47-7 : No portar licencia",
            "ART 146/02 : Sin marbete de seguro",
            "ART 27-3 : Sin matrícula",
            "ART 27-4 : Sin placa",
            "ART 110 : Sin revista",
            "ART 1 LEY513 : Sin tablilla",
            "ART 135C : Sin casco",
            "ART 33-77 : Pirata no rotuló",
            "ART 88 : Obstrucción al tránsito",
            "ART 81 : Sitio (zona) prohibido",
            "ART 97 : Señales de tránsito",
            "ART 97-A : Violar señal de pare",
            "ART 76-C : Viraje en U",
            "ART 27-22 : Una placa",
            "ART 27-2 : Uso distinto de matrícula",
            "ART 27-27 : Color distinto de matrícula",
            "ART 174 : Personas sobre la carga",
            "ART 162 : Ruido",
            "ART 74-H : Ceder paso a vehículo de emergencia",
            "ART 27-4 : No exhibir la placa en su lugar",
            "ART 144 : Luz delantera apagada",
            "ART 145 : Luz trasera",
            "ART 158 : Espejo retrovisor",
            "ART 164-B : Extinguidor",
            "ART 164 AP 200 : Botiquín",
            "ART 171 A-3 : Bandera roja",
            "ART 173-G : Plataforma y timbre",
            "ART 173-D : Peón",
            "ART 164-D : Triángulo",
            "ART 130 LEY 241 : Ventas vías públicas",
            "ART 130 : Lanzar desperdicios en vías públicas",
            "ART 62 : Velocidad muy reducida",
            "ART 5 : Guía a la izquierda",
            "ART 27 Y 47 : Actos prohibidos",
            "ART 161 : Sin cinturón",
            "ART 96-B-1 : Luz roja",
            "ART 1 LEY 143-01 : Uso del celular",
            "ART 106 : Niños menores en el asiento delantero"
            };
    }


    private async void IrAtras2(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}



