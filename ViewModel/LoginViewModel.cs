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

        public LoginPage LoginPage { get; set; }


        [RelayCommand]
        public async Task Entrar()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Cedula) && !string.IsNullOrWhiteSpace(Password))
                {
                    LoginPage?.MostrarPopup(); // Muestra el popup

                    User user = await loginservice.Login(Cedula, Password);



                    if (user != null)
                    {
                        if (Preferences.ContainsKey(nameof(App.user)))
                            Preferences.Remove(nameof(App.user));

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
                        LoginPage?.OcultarPopup(); // Oculta el popup después del login
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
            catch (Exception ex)
            {
                LoginPage?.OcultarPopup(); // Asegúrate de ocultarlo si hay error
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
