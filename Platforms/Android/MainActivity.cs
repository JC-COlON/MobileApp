using Android.App;
using Android.Content;
using Android.OS;
using Android.Net;  // Asegúrate de incluir esta referencia
using AndroidX.Activity;
using Android.Content.PM;
using System.Diagnostics;
using DigesettAPP.Views;  // Asegúrate de incluir esta referencia para Debug.WriteLine

namespace DigesettAPP
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Verifica si la actividad fue lanzada desde un Deep Link
            var intent = Intent;
            var action = intent.Action;
            var data = intent.DataString;

            if (Intent.ActionView == action && data != null)
            {
                // Procesar el Deep Link
                Android.Net.Uri uri = Android.Net.Uri.Parse(data);  // Usa Android.Net.Uri explícitamente
                var token = uri.GetQueryParameter("token");

                // Aquí puedes utilizar el token para restablecer la contraseña o lo que necesites
                System.Diagnostics.Debug.WriteLine($"Token recibido: {token}");


                // Ahora, navega a la página CambiarLaContrasena con el token
                // Primero, asegúrate de que tu página CambiarLaContrasena acepte un token en su constructor

                var page = new CambiarLaContrasena(token); // Pasando el token a la página
                Microsoft.Maui.Controls.Application.Current.MainPage.Navigation.PushAsync(page);
                // Navegar a la página de restablecimiento
            }
        }
    }
}
