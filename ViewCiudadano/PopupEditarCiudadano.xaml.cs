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



    private async void ActualizarButton_Clicked(object sender, EventArgs e)
    {
        LoadingOverlay.IsVisible = true; // Mostrar overlay de carga
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

            // 🛑 Validaciones de campos vacíos
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Correo y teléfono no pueden estar vacíos.", "OK");
                return;
            }

            // 📧 Validación de formato de correo
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(correo))
            {
                await Application.Current.MainPage.DisplayAlert("Email inválido", "Ingrese un email válido en el formato ejemplo@dominio.com", "OK");
                return;
            }

            // 📞 Validación de longitud del teléfono
            if (telefono.Length != 10 || !telefono.All(char.IsDigit))
            {
                await Application.Current.MainPage.DisplayAlert("Teléfono inválido", "El número debe tener exactamente 10 dígitos.", "OK");
                return;
            }

            // 🔄 Obtener datos actuales
            var datosActuales = await ObtenerDatosUsuario(cedula);
            if (datosActuales == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudieron obtener los datos actuales del usuario.", "OK");
                return;
            }

            var datos = new
            {
                Cedula = cedula,
                Email = correo,
                Phone = telefono,
                Name = datosActuales.Name,
                Lastname = datosActuales.Lastname
            };

            var json = JsonSerializer.Serialize(datos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PutAsync($"https://e359-38-158-200-68.ngrok-free.app/api/User/UpdateCiudadanoAPP/{cedula}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseBody);
                var token = doc.RootElement.GetProperty("token").GetString();

                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("AuthToken", token); // 🔐 Guarda el nuevo token
                }

                await Application.Current.MainPage.DisplayAlert("Éxito", "Información actualizada correctamente.", "OK");

                // ✅ Cerrar popup y devolver "true"
                Close(true);
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();

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
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar la información. Verifique los datos ingresados.", "OK");
                }

            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Excepción", $"Ocurrió un error: {ex.Message}", "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false; // Ocultar overlay
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
            var response = await client.GetAsync($"https://e359-38-158-200-68.ngrok-free.app/api/User/{cedula}");

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
    public string Cedula { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
}
