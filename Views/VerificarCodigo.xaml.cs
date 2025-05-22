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
        string email = Preferences.Get("CorreoParaOtp", string.Empty);

        if (otpCode.Length != 6 || string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Por favor ingresa los 6 dígitos del código.", "OK");
            return;
        }

        LoadingOverlay.IsVisible = true;
        Preferences.Set("otpCode", otpCode);

        var payload = new
        {
            emailAddress = email,
            otpCode = otpCode
        };

        try
        {
            using var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };

            HttpResponseMessage response = null;
            bool success = false;
            int attempt = 0;
            int maxRetries = 3;

            while (attempt < maxRetries && !success)
            {
                try
                {
                    var json = JsonSerializer.Serialize(payload);
                    using var content = new StringContent(json, Encoding.UTF8, "application/json");

                    response = await httpClient.PostAsync("https://digesett.somee.com/api/User/verify-otp", content);

                    if (response.IsSuccessStatusCode)
                    {
                        success = true;
                        break;
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", $"Código incorrecto o expirado: {error}", "OK");
                        break; // No reintentar si el código es incorrecto
                    }
                }
                catch (HttpRequestException)
                {
                    attempt++;
                    if (attempt < maxRetries)
                        await Task.Delay(1000);
                }
                catch (TaskCanceledException)
                {
                    attempt++;
                    if (attempt < maxRetries)
                        await Task.Delay(1000);
                }
            }

            if (success)
            {
                await DisplayAlert("Éxito", "Código verificado correctamente.", "OK");
                await Shell.Current.GoToAsync(nameof(CrearNuevaContrasena));
            }
            else if (response == null || !response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "No se pudo verificar el código después de varios intentos. Verifica tu conexión.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema inesperado: {ex.Message}", "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }




    private async void OnReenviarTapped(object sender, EventArgs e)
    {
        string email = Preferences.Get("CorreoParaOtp", string.Empty);

        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "No se encontró un correo para reenviar el código.", "OK");
            return;
        }

        LoadingOverlay.IsVisible = true;

        var payload = new
        {
            emailAddress = email
        };

        try
        {
            using var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(payload);


            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://digesett.somee.com/api/User/request-otp", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Código reenviado", "Revisa tu correo para el nuevo código.", "OK");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo reenviar el código: {error}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error de red: {ex.Message}", "OK");
        }
        finally
        {
            LoadingOverlay.IsVisible = false;
        }
    }



}
