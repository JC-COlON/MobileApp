using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigesettAPP.Models;
using DigesettAPP.Service;
using DigesettAPP.Views;
using Newtonsoft.Json;

namespace DigesettAPP.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _cedula = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        readonly ILoginRepository loginservice = new LoginService();

        [RelayCommand]
        public async Task Entrar()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Cedula) && !string.IsNullOrWhiteSpace(Password))
                {
                    User user = await loginservice.Login(Cedula, Password);

                    if (user != null)
                    {
                        // Guardar usuario en Preferences
                        if (Preferences.ContainsKey(nameof(App.user)))
                        {
                            Preferences.Remove(nameof(App.user));
                        }

                        string userDetails = JsonConvert.SerializeObject(user);
                        Preferences.Set(nameof(App.user), userDetails);
                        App.user = user;

                        switch (user.Rol?.ToLower())
                        {
                            case "agente":
                                await Shell.Current.GoToAsync("//MainHome");
                                break;

                            case "ciudadano":
                            case "administrador":
                                await Shell.Current.GoToAsync("//HomeViewCiudadano");
                                break;

                            default:
                                await Shell.Current.DisplayAlert("Acceso Denegado", "No tiene permisos para acceder a esta aplicación.", "OK");
                                break;
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Usuario o contraseña incorrecta", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Debe completar todos los campos.", "OK");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Si el servicio lanza una HttpRequestException y devuelve un contenido
                if (httpEx.Data.Contains("ResponseBody"))
                {
                    var responseBody = httpEx.Data["ResponseBody"].ToString();
                    try
                    {
                        dynamic errorObj = JsonConvert.DeserializeObject(responseBody);
                        string message = errorObj?.message ?? "Error al iniciar sesión.";
                        await Shell.Current.DisplayAlert("Error", message, "OK");
                    }
                    catch
                    {
                        await Shell.Current.DisplayAlert("Error", "Error inesperado al iniciar sesión.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo conectar al servidor.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        public void LimpiarCampos()
        {
            Cedula = string.Empty;
            Password = string.Empty;
        }
    }
}
