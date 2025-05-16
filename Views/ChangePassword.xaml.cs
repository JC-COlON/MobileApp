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
        var currentPassword = Contrase�aActualEntry.Text?.Trim();
        var newPassword = NuevaContrase�aEntry.Text?.Trim();
        var confirmPassword = ConfirmarContrase�aEntry.Text?.Trim();

        // Validaci�n de campos vac�os
        if (string.IsNullOrWhiteSpace(currentPassword) ||
            string.IsNullOrWhiteSpace(newPassword) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
            return;
        }

        // Validaci�n de la longitud de la nueva contrase�a
        if (newPassword.Length < 8)
        {
            await DisplayAlert("Error", "La nueva contrase�a debe tener al menos 8 caracteres.", "OK");
            return;
        }

        // Validaci�n de coincidencia de contrase�as
        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Error", "La nueva contrase�a y la confirmaci�n no coinciden.", "OK");
            return;
        }

        string cedula = ObtenerCedulaDesdeToken();
        if (cedula == "Informaci�n no disponible")
        {
            await DisplayAlert("Error", "No se pudo obtener la c�dula del usuario.", "OK");
            return;
        }

        // Confirmaci�n sin mostrar datos
        bool confirm = await DisplayAlert("�Confirmar cambio?", "�Est�s seguro de cambiar tu contrase�a?", "S�", "Cancelar");
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
                await DisplayAlert("Error", $"No se pudo cambiar la contrase�a.\n{errorContent}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurri� un error inesperado.\n{ex.Message}", "OK");
        }
    }




    private async void OnCancelarTapped(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Regresa a la p�gina anterior
    }



    private string ObtenerCedulaDesdeToken()
    {
        return ObtenerClaimDesdeToken("http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber");
    }

    private string ObtenerClaimDesdeToken(string claimType)
    {
        string token = Preferences.Get("AuthToken", string.Empty);

        if (string.IsNullOrEmpty(token))
            return "Informaci�n no disponible";

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return claimValue ?? "Informaci�n no disponible";
    }
}
