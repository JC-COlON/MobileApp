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
using System.Text;
using System.Net; // NUEVO: para usar ShowPopupAsync

namespace DigesettAPP.ViewModel
{
    public class PagarMultaCiudadanoViewModel : BindableObject
    {
        private string _nombre;
        private string _cedula;
        private string _correo;
        private string _numeroTarjeta;
        private string _expiracion;
        private bool _yaMostroAlerta = false;

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

        private Color _colorNumeroTarjeta = Colors.Black;
        public Color ColorNumeroTarjeta
        {
            get => _colorNumeroTarjeta;
            set
            {
                _colorNumeroTarjeta = value;
                OnPropertyChanged();
            }
        }
        private bool _puedePagar;
        public bool PuedePagar
        {
            get => _puedePagar;
            set { _puedePagar = value; OnPropertyChanged(); }
        }


        // Al inicio del ViewModel:
        private Ticket _ticketSeleccionado;
        public Ticket TicketSeleccionado
        {
            get => _ticketSeleccionado;
            set { _ticketSeleccionado = value; OnPropertyChanged(); }
        }
        public async Task PrepararVista()
        {
            _yaMostroAlerta = false;
            await CargarUltimaTarjeta();
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
            const int maxIntentos = 3;
            const int milisegundosEsperaBase = 1000;

            for (int intento = 1; intento <= maxIntentos; intento++)
            {
                try
                {
                    var cedulaLimpia = Cedula.Replace("Cédula: ", "").Trim();
                    string url = $"https://digesett.somee.com/api/CreditCard/GetCreditCardsByCedula?cedula={cedulaLimpia}";

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
                            Expiracion = $" {ultimaTarjeta.ExpirationDate}";
                            CardId = ultimaTarjeta.CardId;
                            PuedePagar = true;
                            ColorNumeroTarjeta = Colors.Black;
                            return;
                        }
                    }

                    // Si no hay tarjeta
                    NumeroTarjeta = "No hay tarjeta registrada";
                    Expiracion = string.Empty;
                    PuedePagar = false;
                    ColorNumeroTarjeta = Colors.Red;

                    if (!_yaMostroAlerta)
                    {
                        _yaMostroAlerta = true;

                        bool registrar = await Application.Current.MainPage.DisplayAlert(
                            "Sin tarjeta registrada",
                            "Usted no tiene tarjeta registrada para proceder con el pago.\n¿Desea registrar una?",
                            "Sí", "No");

                        if (registrar)
                            await Shell.Current.GoToAsync("AgregarTarjetaCiudadano");
                        else
                            await Shell.Current.GoToAsync("..");
                    }

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Intento {intento}] Error cargando tarjeta: {ex.Message}");

                    if (intento == maxIntentos)
                    {
                        NumeroTarjeta = "Error al cargar";
                        Expiracion = string.Empty;
                        PuedePagar = false;

                        await Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar la tarjeta. Intente más tarde.", "OK");
                    }
                    else
                    {
                        await Task.Delay(milisegundosEsperaBase * intento); // espera progresiva
                    }
                }
            }
        }





        public async Task CargarMultaPorId(int ticketId)
        {
            const int maxIntentos = 3;
            const int milisegundosEsperaBase = 1000;

            for (int intento = 1; intento <= maxIntentos; intento++)
            {
                try
                {
                    IsLoading = true; // Mostrar el loading

                    string url = $"https://digesett.somee.com/api/Ticket/{ticketId}";
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
                            OnPropertyChanged(nameof(TicketSeleccionado));
                            return; // Éxito, salir
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Advertencia", "La respuesta de la API no contenía una multa válida.", "OK");
                            return;
                        }
                    }
                    else
                    {
                        if (intento == maxIntentos)
                        {
                            string errorMsg = await response.Content.ReadAsStringAsync();
                            await App.Current.MainPage.DisplayAlert("Error en la API", $"StatusCode: {response.StatusCode}\nMensaje: {errorMsg}", "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (intento == maxIntentos)
                    {
                        await App.Current.MainPage.DisplayAlert("Excepción", $"Error al obtener la multa:\n{ex.Message}", "OK");
                    }
                }
                finally
                {
                    IsLoading = false; // Ocultar el loading
                }

                await Task.Delay(milisegundosEsperaBase * intento); // espera progresiva
            }
        }



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
            if (string.IsNullOrEmpty(token))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Token no encontrado. El usuario no está autenticado.", "OK");
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadToken(token) as JwtSecurityToken;

            int userId = int.Parse(jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ?? "0");
            string email = jwt?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value ?? "";

            if (userId == 0 || string.IsNullOrEmpty(email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo extraer el UserId o el Email del token.", "OK");
                return;
            }

            int cardId = CardId;
            int ticketId = TicketSeleccionado.TicketId;

            var payload = new
            {
                TicketId = ticketId,
                UserId = userId,
                CardId = cardId,
                Email = email
            };

            string jsonPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);

            IsLoading = true;
            HttpResponseMessage response = null;
            int maxRetries = 3;
            bool pagoExitoso = false;

            try
            {
                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using var client = new HttpClient
                        {
                            Timeout = TimeSpan.FromSeconds(60)
                        };

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                        response = await client.PostAsync("https://digesett.somee.com/api/Payment/PayTicket", content);

                        var responseContent = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            pagoExitoso = true;
                            break;
                        }
                        else if (responseContent.Contains("ya ha sido pagada", StringComparison.OrdinalIgnoreCase))
                        {
                            pagoExitoso = true;
                            break;
                        }
                        else if (attempt == maxRetries)
                        {
                            var debugInfo = $"Status: {response.StatusCode}\n" +
                                            $"Payload: {jsonPayload}\n" +
                                            $"Response: {responseContent}";

                            await App.Current.MainPage.DisplayAlert("Error al pagar", debugInfo, "OK");
                        }
                    }
                    catch (TaskCanceledException tex)
                    {
                        if (attempt == maxRetries)
                        {
                            await App.Current.MainPage.DisplayAlert("Tiempo de espera agotado", "El servidor tardó demasiado en responder.", "OK");
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        if (attempt == maxRetries)
                        {
                            await App.Current.MainPage.DisplayAlert("Error de conexión", $"No se pudo conectar con el servidor:\n{ex.Message}", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (attempt == maxRetries)
                        {
                            await App.Current.MainPage.DisplayAlert("Error inesperado", $"Ocurrió un error:\n{ex.Message}", "OK");
                        }
                    }

                    await Task.Delay(1000 * attempt); // espera progresiva entre intentos
                }

                if (pagoExitoso)
                {
                    var popup = new ViewCiudadano.PoputMultaPagadaExito();
                    await Application.Current.MainPage.ShowPopupAsync(popup);
                }
            }
            finally
            {
                IsLoading = false;
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
