using CommunityToolkit.Maui.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DigesettAPP.ViewCiudadano;

public partial class PopupEditarCiudadano : Popup
{
	public PopupEditarCiudadano()
	{
		InitializeComponent();
        CargarDatosUsuario();
    }



    private static readonly HttpClient httpClient = new HttpClient();





    private async void ActualizarButton_Clicked(object sender, EventArgs e)
    {
        LoadingOverlay.IsVisible = true;
        try
        {
            string cedula = ObtenerCedulaDelToken();
            if (string.IsNullOrEmpty(cedula))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
                return;
            }

            var correo = CorreoEntry.Text?.Trim();
            var telefono = TelefonoEntry.Text?.Trim();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Correo y teléfono no pueden estar vacíos.", "OK");
                return;
            }

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(correo))
            {
                await Application.Current.MainPage.DisplayAlert("Email inválido", "Ingrese un email válido en el formato ejemplo@dominio.com", "OK");
                return;
            }

            if (telefono.Length != 10 || !telefono.All(char.IsDigit))
            {
                await Application.Current.MainPage.DisplayAlert("Teléfono inválido", "El número debe tener exactamente 10 dígitos.", "OK");
                return;
            }

            var datosActuales = await ObtenerDatosUsuario(cedula);
            if (datosActuales == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudieron obtener los datos actuales del usuario.", "OK");
                return;
            }

            var datos = new
            {
                Cedula = cedula,
                Name = datosActuales.Name,
                Lastname = datosActuales.Lastname,
                Password = "",
                RolId = datosActuales.RolId,
                Email = correo,
                Phone = telefono,
                ProfileImg = datosActuales.ProfileImg ?? "string",
                NacionalityId = datosActuales.NacionalityId,
                Birthdate = datosActuales.Birthdate?.ToString("yyyy-MM-dd") ?? "2000-01-01",
                GenderId = datosActuales.GenderId,
                BloodTypeId = datosActuales.BloodTypeId,
                Height = datosActuales.Height,
                OfficeId = datosActuales.OfficeId,
                CivilStatusId = datosActuales.CivilStatusId,
                NoAgente = datosActuales.NoAgente ?? "string",
                StatusId = datosActuales.StatusId
            };



            var json = JsonSerializer.Serialize(datos, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });


            var content = new StringContent(json, Encoding.UTF8, "application/json");




            HttpResponseMessage response = null;
            int maxRetries = 3;
            int attempt = 0;
            bool success = false;

            while (attempt < maxRetries && !success)
            {
                try
                {
                    response = await httpClient.PutAsync($"https://digesett.somee.com/api/User/UpdateCiudadanoAPP/{cedula}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        success = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (HttpRequestException)
                {
                    attempt++;
                    if (attempt < maxRetries)
                        await Task.Delay(1000);
                }
            }

            if (success && response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseBody);
                var token = doc.RootElement.GetProperty("token").GetString();

                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("AuthToken", token);
                }

                await Application.Current.MainPage.DisplayAlert("Éxito", "Información actualizada correctamente.", "OK");
                Close(true);
            }
            else if (response != null)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                string mensajeError = errorBody;

                try
                {
                    using var doc = JsonDocument.Parse(errorBody);
                    if (doc.RootElement.TryGetProperty("message", out var msgProp))
                        mensajeError = msgProp.GetString() ?? errorBody;
                    else if (doc.RootElement.TryGetProperty("error", out var errProp))
                        mensajeError = errProp.GetString() ?? errorBody;
                }
                catch { }

                if (errorBody.Contains("unique_telefono", StringComparison.OrdinalIgnoreCase))
                {
                    await Application.Current.MainPage.DisplayAlert("Teléfono duplicado", "Este número ya está registrado.", "OK");
                }
                else if (errorBody.Contains("unique_correo", StringComparison.OrdinalIgnoreCase))
                {
                    await Application.Current.MainPage.DisplayAlert("Correo duplicado", "Este correo ya está registrado.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error al actualizar", mensajeError, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar la información después de varios intentos. Verifica tu conexión.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Excepción", $"Ocurrió un error: {ex.Message}", "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }






    private string ObtenerCedulaDelToken()
    {
        try
        {
            string token = Preferences.Get("AuthToken", string.Empty);
            if (string.IsNullOrEmpty(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var cedulaClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");

            return cedulaClaim?.Value;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la cédula del token: {ex.Message}");
            return null;
        }
    }

    private async void CerrarButton_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(true); // true indica que se hizo una actualización
                                // Cierra el popup
    }

    private async Task<UsuarioDTO> ObtenerDatosUsuario(string cedula)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://digesett.somee.com/api/User/{cedula}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<UsuarioDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return usuario;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo obtener los datos del usuario.", "OK");
                return null;
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Excepción", $"Ocurrió un error: {ex.Message}", "OK");
            return null;
        }
    }

    private async void CargarDatosUsuario()
    {
        string cedula = ObtenerCedulaDelToken();
        if (string.IsNullOrEmpty(cedula))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo obtener la cédula del token.", "OK");
            return;
        }

        var usuario = await ObtenerDatosUsuario(cedula);
        if (usuario != null)
        {
            CorreoEntry.Text = usuario.Email;
            TelefonoEntry.Text = usuario.Phone;
        }
    }


}
public class UsuarioDTO
{
    public string Cedula { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    // Hacer Password nullable y opcional
    public string? Password { get; set; }  // <-- Cambio aquí

    public int RolId { get; set; }
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public string? ProfileImg { get; set; }
    public int? NacionalityId { get; set; }
    public DateOnly? Birthdate { get; set; }
    public int? GenderId { get; set; }
    public int? BloodTypeId { get; set; }
    public decimal? Height { get; set; }
    public int? OfficeId { get; set; }
    public int? CivilStatusId { get; set; }
    public string? NoAgente { get; set; }
    public int? StatusId { get; set; }
}
