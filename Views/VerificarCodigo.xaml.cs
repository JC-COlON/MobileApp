using System.Text;
using System.Text.Json;

namespace DigesettAPP.Views;

public partial class VerificarCodigo : ContentPage
{
    public VerificarCodigo()
    {
        InitializeComponent();
    }

    private void OtpDigitChanged(object sender, TextChangedEventArgs e)
    {
        var current = sender as Entry;

        // Avanzar si hay un carácter
        if (!string.IsNullOrWhiteSpace(current.Text))
        {
            switch (current)
            {
                case var _ when current == Digit1: Digit2.Focus(); break;
                case var _ when current == Digit2: Digit3.Focus(); break;
                case var _ when current == Digit3: Digit4.Focus(); break;
                case var _ when current == Digit4: Digit5.Focus(); break;
                case var _ when current == Digit5: Digit6.Focus(); break;
            }
        }

        // Retroceder si se borró y el campo está vacío
        if (string.IsNullOrEmpty(e.NewTextValue) && !string.IsNullOrEmpty(e.OldTextValue))
        {
            switch (current)
            {
                case var _ when current == Digit6: Digit5.Focus(); break;
                case var _ when current == Digit5: Digit4.Focus(); break;
                case var _ when current == Digit4: Digit3.Focus(); break;
                case var _ when current == Digit3: Digit2.Focus(); break;
                case var _ when current == Digit2: Digit1.Focus(); break;
            }
        }
    }

    private async void OnVerificarClicked(object sender, EventArgs e)
    {
        string otpCode = Digit1.Text + Digit2.Text + Digit3.Text + Digit4.Text + Digit5.Text + Digit6.Text;
        string email = Preferences.Get("CorreoParaOtp", string.Empty); // <-- Asegúrate que este valor se guarda antes

        if (otpCode.Length != 6 || string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Por favor ingresa los 6 dígitos del código.", "OK");
            return;
        }

        // Mostrar overlay de carga
        LoadingOverlay.IsVisible = true;

        Preferences.Set("otpCode", otpCode);

        var payload = new
        {
            emailAddress = email,
            otpCode = otpCode
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("https://5fce-200-215-234-53.ngrok-free.app/api/User/verify-otp", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Código verificado correctamente.", "OK");
                await Shell.Current.GoToAsync(nameof(CrearNuevaContrasena));
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"Verificación fallida: {error}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al verificar el código: {ex.Message}", "OK");
        }
        finally
        {
            // Ocultar overlay de carga
            LoadingOverlay.IsVisible = false;
        }
    }
}
