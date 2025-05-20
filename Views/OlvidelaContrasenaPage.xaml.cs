using System.Buffers.Text;
using System.Text;
using System.Text.Json;

namespace DigesettAPP.Views;

public partial class OlvidelaContrasenaPage : ContentPage
{

   
    public OlvidelaContrasenaPage()
	{
		InitializeComponent();
	}

    // Acción para enviar el correo de recuperación
    private async void OnEnviarClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Por favor ingresa tu correo electrónico.", "OK");
            return;
        }

        var dto = new ForgotmyPasswordOtpDTOs
        {
            EmailAddress = email
        };

        try
        {
            LoadingOverlay.IsVisible = true;

            using var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10) // Ajusta el timeout si lo deseas
            };

            int maxRetries = 3;
            int attempt = 0;
            bool success = false;
            HttpResponseMessage response = null;

            while (attempt < maxRetries && !success)
            {
                try
                {
                    var json = JsonSerializer.Serialize(dto);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");

                    response = await httpClient.PostAsync("https://digesett.somee.com/api/User/request-otp", content);

                    if (response.IsSuccessStatusCode)
                    {
                        success = true;
                        break;
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", $"Intento {attempt + 1}: No se pudo enviar el código. {error}", "OK");
                        break; // No reintentes si no es error de red
                    }
                }
                catch (HttpRequestException)
                {
                    attempt++;
                    if (attempt < maxRetries)
                        await Task.Delay(1000);
                }
                catch (TaskCanceledException) // Maneja los timeouts
                {
                    attempt++;
                    if (attempt < maxRetries)
                        await Task.Delay(1000);
                }
            }

            if (success)
            {
                Preferences.Set("CorreoParaOtp", email);
                await DisplayAlert("Éxito", "Se ha enviado un código de verificación a tu correo.", "OK");
                await Shell.Current.GoToAsync(nameof(VerificarCodigo));
            }
            else if (response == null || !response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "No se pudo enviar el código después de varios intentos. Verifica tu conexión.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }





}

public class ForgotmyPasswordOtpDTOs
{
    public string EmailAddress { get; set; }
}
