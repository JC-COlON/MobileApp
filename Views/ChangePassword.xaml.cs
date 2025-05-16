using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Net.Http.Headers;
using CommunityToolkit.Maui.Views;

namespace DigesettAPP.Views;

public partial class ChangePassword : ContentPage
{
    public ChangePassword()
    {
        InitializeComponent();
    }

    private async void OnActualizarClicked(object sender, EventArgs e)
    {
        var currentPassword = ContraseñaActualEntry.Text?.Trim();
        var newPassword = NuevaContraseñaEntry.Text?.Trim();
        var confirmPassword = ConfirmarContraseñaEntry.Text?.Trim();

        // Validación de campos vacíos
        if (string.IsNullOrWhiteSpace(currentPassword) ||
            string.IsNullOrWhiteSpace(newPassword) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
            return;
        }

        // Validación de la longitud de la nueva contraseña
        if (newPassword.Length < 8)
        {
            await DisplayAlert("Error", "La nueva contraseña debe tener al menos 8 caracteres.", "OK");
            return;
        }

        // Validación de coincidencia de contraseñas
        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Error", "La nueva contraseña y la confirmación no coinciden.", "OK");
            return;
        }

        string cedula = ObtenerCedulaDesdeToken();
        if (cedula == "Información no disponible")
        {
            await DisplayAlert("Error", "No se pudo obtener la cédula del usuario.", "OK");
            return;
        }

        // Confirmación sin mostrar datos
        bool confirm = await DisplayAlert("¿Confirmar cambio?", "¿Estás seguro de cambiar tu contraseña?", "Sí", "Cancelar");
        if (!confirm)
            return;

        var requestData = new
        {
            identification = cedula,
            currentPassword = currentPassword,
            newPassword = newPassword,
            confirmPassword = confirmPassword
        };

        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.PostAsync("https://5fce-200-215-234-53.ngrok-free.app/api/User/change-password", content);

            if (response.IsSuccessStatusCode)
            {
                // Mostrar el pop-up
                var popup = new PoputCambiarContrasena();
                await Application.Current.MainPage.ShowPopupAsync(popup);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo cambiar la contraseña.\n{errorContent}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error inesperado.\n{ex.Message}", "OK");
        }
    }




    private async void OnCancelarTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Regresa a la página anterior
    }



    private string ObtenerCedulaDesdeToken()
    {
        return ObtenerClaimDesdeToken("http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");
    }

    private string ObtenerClaimDesdeToken(string claimType)
    {
        string token = Preferences.Get("AuthToken", string.Empty);

        if (string.IsNullOrEmpty(token))
            return "Información no disponible";

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return claimValue ?? "Información no disponible";
    }
}
