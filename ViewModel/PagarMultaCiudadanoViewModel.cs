using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using DigesettAPP.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using CommunityToolkit.Maui.Views;
using System.Text; // NUEVO: para usar ShowPopupAsync

namespace DigesettAPP.ViewModel
{
    public class PagarMultaCiudadanoViewModel : BindableObject
    {
        private string _nombre;
        private string _cedula;
        private string _correo;
        private string _numeroTarjeta;
        private string _expiracion;

        public string NombreCompleto
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(); }
        }

        private int _cardId; // Propiedad para almacenar el CardId
        public int CardId
        {
            get => _cardId;
            set { _cardId = value; OnPropertyChanged(); }
        }

        public string Cedula
        {
            get => _cedula;
            set { _cedula = value; OnPropertyChanged(); }
        }

        public string Correo
        {
            get => _correo;
            set { _correo = value; OnPropertyChanged(); }
        }

        public string NumeroTarjeta
        {
            get => _numeroTarjeta;
            set { _numeroTarjeta = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();  // Notifica a la vista que la propiedad ha cambiado
            }
        }


        // Al inicio del ViewModel:
        private Ticket _ticketSeleccionado;
        public Ticket TicketSeleccionado
        {
            get => _ticketSeleccionado;
            set { _ticketSeleccionado = value; OnPropertyChanged(); }
        }

        public string Expiracion
        {
            get => _expiracion;
            set { _expiracion = value; OnPropertyChanged(); }
        }

        public ICommand CargarDatosCommand { get; }
        public ICommand PagarMultaCommand { get; } // NUEVO

        public PagarMultaCiudadanoViewModel()
        {
            CargarDatosCommand = new Command(async () => await CargarDatos());
            PagarMultaCommand = new Command(async () => await PagarMulta()); // NUEVO
            _ = CargarDatos();
        }

        public async Task CargarDatos()
        {
            try
            {
                string token = Preferences.Get("AuthToken", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadToken(token) as JwtSecurityToken;

                var nombre = jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
                var apellido = jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
                Cedula = jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber")?.Value;
                Correo = jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

                NombreCompleto = $" {nombre} {apellido}";
                Cedula = $" {Cedula}";
                Correo = $" {Correo}";

                await CargarUltimaTarjeta();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando datos del token: {ex.Message}");
            }
        }

        private async Task CargarUltimaTarjeta()
        {
            try
            {
                var cedulaLimpia = Cedula.Replace("Cédula: ", "").Trim();
                string url = $"https://d79f-200-215-234-53.ngrok-free.app/api/CreditCard/GetCreditCardsByCedula?cedula={cedulaLimpia}";

                using var client = new HttpClient();
                string token = Preferences.Get("AuthToken", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var tarjetas = JsonConvert.DeserializeObject<List<CreditCard>>(json);

                    var ultimaTarjeta = tarjetas?.LastOrDefault();
                    if (ultimaTarjeta != null)
                    {
                        string num = ultimaTarjeta.CardNumber.ToString();
                        NumeroTarjeta = $"**** **** **** {num.Substring(num.Length - 4)}";
                        Expiracion = $"Exp: {ultimaTarjeta.ExpirationDate}";

                        // Asignamos el CardId a la propiedad en ViewModel
                        CardId = ultimaTarjeta.CardId;  // Aquí es donde asignas el CardId
                        Console.WriteLine($"ID de la tarjeta seleccionada: {CardId}");  // Solo para depurar, puedes eliminarlo luego.
                    }
                    else
                    {
                        NumeroTarjeta = "No hay tarjetas registradas";
                        Expiracion = string.Empty;
                    }
                }
                else
                {
                    NumeroTarjeta = "Error cargando tarjeta";
                    Expiracion = "";
                }
            }
            catch (Exception ex)
            {
                NumeroTarjeta = "Error de conexión";
                Console.WriteLine($"Error cargando tarjeta: {ex.Message}");
            }
        }



        public async Task CargarMultaPorId(int ticketId)
        {
            try
            {
                string url = $"https://d79f-200-215-234-53.ngrok-free.app/api/Ticket/{ticketId}";

                using var client = new HttpClient();
                string token = Preferences.Get("AuthToken", string.Empty);

                if (string.IsNullOrEmpty(token))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No hay token de autenticación disponible.", "OK");
                    return;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var ticket = JsonConvert.DeserializeObject<Ticket>(json);

                    if (ticket != null)
                    {
                        TicketSeleccionado = ticket;
                        OnPropertyChanged(nameof(TicketSeleccionado)); // Asegura actualización en la UI
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Advertencia", "La respuesta de la API no contenía una multa válida.", "OK");
                    }
                }
                else
                {
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    await App.Current.MainPage.DisplayAlert("Error en la API", $"StatusCode: {response.StatusCode}\nMensaje: {errorMsg}", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Excepción", $"Error al obtener la multa:\n{ex.Message}", "OK");
            }
        }

        // NUEVO: Método para pagar la multa
        public async Task PagarMulta()
        {
            if (TicketSeleccionado == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No hay una multa seleccionada para pagar.", "OK");
                return;
            }

            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmación", "¿Seguro que quiere pagar esta multa?", "Sí", "No");
            if (!confirm) return;

            string token = Preferences.Get("AuthToken", string.Empty);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadToken(token) as JwtSecurityToken;

            // Obtener los valores del token
            int userId = int.Parse(jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "0");
            string email = jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "";

            // Obtener el CardId desde el ViewModel
            int cardId = CardId; // Usamos la propiedad CardId del ViewModel
            int ticketId = TicketSeleccionado.TicketId; // Obtener el ticketId de la multa seleccionada

            // Crear el objeto payload
            var payload = new
            {
                ticketId = ticketId,
                userId = userId,
                cardId = cardId,
                email = email
            };

            // Serializar el objeto a JSON
            string jsonPayload = JsonConvert.SerializeObject(payload);

            try
            {
                // Activar el loading antes de realizar la solicitud HTTP
                IsLoading = true;

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://d79f-200-215-234-53.ngrok-free.app/api/Payment/PayTicket", content);

                // Desactivar el loading después de que se haya recibido la respuesta
                IsLoading = false;

                if (response.IsSuccessStatusCode)
                {
                    // Mostrar pop-up personalizado
                    var popup = new ViewCiudadano.PoputMultaPagadaExito();
                    await App.Current.MainPage.ShowPopupAsync(popup);
                }
                else
                {
                    // Si la respuesta no es exitosa, obtener y mostrar el error
                    string errorContent = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"Error al pagar la multa. StatusCode: {response.StatusCode}\nMensaje: {errorContent}";

                    // Mostrar el mensaje de error
                    await App.Current.MainPage.DisplayAlert("Error en la API", errorMessage, "OK");
                }
            }
            catch (HttpRequestException ex)
            {
                // Captura cualquier error relacionado con la solicitud HTTP (por ejemplo, problemas de red)
                IsLoading = false;  // Asegúrate de desactivar el loading si ocurre un error de red
                await App.Current.MainPage.DisplayAlert("Error de conexión", $"No se pudo conectar con el servidor. Detalles: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Captura errores generales que no sean específicamente HTTP
                IsLoading = false;  // Asegúrate de desactivar el loading si ocurre un error inesperado
                await App.Current.MainPage.DisplayAlert("Error desconocido", $"Ocurrió un error inesperado. Detalles: {ex.Message}", "OK");
            }
        }



    }


}


public class CreditCard
{
    public int CardId { get; set; } // Esta propiedad debe existir
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
}
