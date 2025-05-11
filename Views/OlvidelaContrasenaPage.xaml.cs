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
            // Mostrar loading
            LoadingOverlay.IsVisible = true;

            var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://5fce-200-215-234-53.ngrok-free.app/api/User/request-otp", content);

            if (response.IsSuccessStatusCode)
            {
                Preferences.Set("CorreoParaOtp", email);
                await DisplayAlert("Éxito", "Se ha enviado un código de verificación a tu correo.", "OK");

                await Shell.Current.GoToAsync(nameof(VerificarCodigo));
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo enviar el código. {errorResponse}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
        finally
        {
            // Ocultar loading
            LoadingOverlay.IsVisible = false;
        }
    }



}

public class ForgotmyPasswordOtpDTOs
{
    public string EmailAddress { get; set; }
}
