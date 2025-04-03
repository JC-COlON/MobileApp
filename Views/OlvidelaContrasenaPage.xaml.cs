using System.Buffers.Text;
using System.Text;
using System.Text.Json;

namespace DigesettAPP.Views;

public partial class OlvidelaContrasenaPage : ContentPage
{

    private const string BaseUrl = "https://digesett.somee.com/api/forgot-password";
    public OlvidelaContrasenaPage()
	{
		InitializeComponent();
	}

    // Acción para enviar el correo de recuperación
    private async void OnEnviarClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim();
        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Por favor, ingresa un correo válido.", "OK");
            return;
        }

        var request = new
        {
            emailAddress = email,
            clientURL = "https://tuapp.com" // Asegúrate de que esta URL sea la de tu aplicación
        };

        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Se ha enviado un correo con las instrucciones para restablecer la contraseña.", "OK");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo enviar el correo: {error}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error de conexión: {ex.Message}", "OK");
        }
    }


    private async void OnReenviarTapped(object sender, EventArgs e)
    {
        // Aquí puedes volver a llamar la lógica de reenvío de correo
        await DisplayAlert("Reenviar", "Se ha enviado un nuevo correo de recuperación.", "OK");
    }


}