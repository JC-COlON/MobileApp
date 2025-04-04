using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigesettAPP.Models
{
    public static class DataPicker
    {
        // Diccionario para el tipo de vehículo
        public static Dictionary<string, int> tipoVehiculoMap = new Dictionary<string, int>
    {
        { "Motocicleta", 1 },
        { "Automóvil", 2 },
        { "Camioneta", 3 },
        { "Autobús", 4 },
        { "Otros", 5 }
    };

        // Diccionario para el artículo infringido
        public static Dictionary<string, int> articuloInfringidoMap = new Dictionary<string, int>
    {
        { "ART 29 : Sin licencia", 1 },
        { "ART 40-41 : Licencia vencida", 2 },
        { "ART 47-7 : No portar licencia", 3 },
        { "ART 146/02 : Sin marbete de seguro", 4 },
        { "ART 27-3 : Sin matrícula", 5 },
        { "ART 27-4 : Sin placa", 6 },
        { "ART 110 : Sin revista", 7 },
        { "ART 1 LEY513 : Sin tablilla", 8 },
        { "ART 135C : Sin casco", 9 },
        { "ART 33-77 : Pirata no rotuló", 10 },
        { "ART 88 : Obstrucción al tránsito", 11 },
        { "ART 81 : Sitio (zona) prohibido", 12 },
        { "ART 97 : Señales de tránsito", 13 },
        { "ART 97-A : Violar señal de pare", 14 },
        { "ART 76-C : Viraje en U", 15 },
        { "ART 27-22 : Una placa", 16 },
        { "ART 27-2 : Uso distinto de matrícula", 17 },
        { "ART 27-27 : Color distinto de matrícula", 18 },
        { "ART 174 : Personas sobre la carga", 19 },
        { "ART 162 : Ruido", 20 },
        { "ART 74-H : Ceder paso a vehículo de emergencia", 21 },
        { "ART 27-4 : No exhibir la placa en su lugar", 22 },
        { "ART 144 : Luz delantera apagada", 23 },
        { "ART 145 : Luz trasera", 24 },
        { "ART 158 : Espejo retrovisor", 25 },
        { "ART 164-B : Extinguidor", 26 },
        { "ART 164 AP 200 : Botiquín", 27 },
        { "ART 171 A-3 : Bandera roja", 28 },
        { "ART 173-G : Plataforma y timbre", 29 },
        { "ART 173-D : Peón", 30 },
        { "ART 164-D : Triángulo", 31 },
        { "ART 130 LEY 241 : Ventas vías públicas", 32 },
        { "ART 130 : Lanzar desperdicios en vías públicas", 33 },
        { "ART 62 : Velocidad muy reducida", 34 },
        { "ART 5 : Guía a la izquierda", 35 },
        { "ART 27 Y 47 : Actos prohibidos", 36 },
        { "ART 161 : Sin cinturón", 37 },
        { "ART 96-B-1 : Luz roja", 38 },
        { "ART 1 LEY 143-01 : Uso del celular", 39 },
        { "ART 106 : Niños menores en el asiento delantero", 40 }
    };
    }
}
