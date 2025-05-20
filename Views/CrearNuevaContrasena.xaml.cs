using System.Text;
using System.Text.Json;

namespace DigesettAPP.Views;

public partial class CrearNuevaContrasena : ContentPage
{
    bool isNuevaPasswordHidden = true;
    bool isConfirmarPasswordHidden = true;

    public CrearNuevaContrasena()
    {
        InitializeComponent();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var email = Preferences.Get("CorreoParaOtp", string.Empty);
        var otpCode = Preferences.Get("otpCode", string.Empty);

        var newPassword = NuevaContrasenaEntry.Text?.Trim();
        var confirmPassword = ConfirmarContrasenaEntry.Text?.Trim();

        // Validar campos vac�os
        if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            await DisplayAlert("Error", "Ambos campos de contrase�a deben ser llenados.", "OK");
            return;
        }

        // Validar longitud m�nima
        if (newPassword.Length < 6)
        {
            await DisplayAlert("Error", "La contrase�a debe tener al menos 6 caracteres.", "OK");
            return;
        }

        // Validar coincidencia
        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Error", "Las contrase�as no coinciden.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otpCode))
        {
            await DisplayAlert("Error", "Faltan datos de verificaci�n. Intente el proceso nuevamente.", "OK");
            return;
        }

        // Mostrar overlay de carga
        LoadingOverlay.IsVisible = true;

        try
        {
            var exito = await EnviarNuevaContrasenaAsync(email, otpCode, newPassword);
            if (exito)
            {
                await DisplayAlert("�xito", "Contrase�a cambiada exitosamente. Inicia sesi�n con tu nueva contrase�a.", "OK");

                // Limpia historial de navegaci�n y lleva a LoginPage
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
        finally
        {
            // Ocultar overlay
            LoadingOverlay.IsVisible = false;
        }
    }

    private async Task<bool> EnviarNuevaContrasenaAsync(string email, string otpCode, string newPassword)
    {
        try
        {
            var dto = new ResetPasswordOtpDTO
            {
                EmailAddress = email,
                OtpCode = otpCode,
                NewPassword = newPassword
            };

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync("https://digesett.somee.com/api/User/reset-passwordotp", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo cambiar la contrase�a. {errorResponse}", "OK");
                return false;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "OK");
            return false;
        }
    }

    private void OjoNuevaContrasena_Clicked(object sender, EventArgs e)
    {
        isNuevaPasswordHidden = !isNuevaPasswordHidden;
        NuevaContrasenaEntry.IsPassword = isNuevaPasswordHidden;

        OjoNuevaContrasena.Source = isNuevaPasswordHidden ? "ojoabierto.png" : "ojocerrado.png";
    }

    private void OjoConfirmarContrasena_Clicked(object sender, EventArgs e)
    {
        isConfirmarPasswordHidden = !isConfirmarPasswordHidden;
        ConfirmarContrasenaEntry.IsPassword = isConfirmarPasswordHidden;

        OjoConfirmarContrasena.Source = isConfirmarPasswordHidden ? "ojoabierto.png" : "ojocerrado.png";
    }
}

public class ResetPasswordOtpDTO
{
    public string EmailAddress { get; set; }
    public string OtpCode { get; set; }
    public string NewPassword { get; set; }
}
