

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
                        if (user.Rol != "Agente") // Verifica que el usuario sea un agente
                        {
                            await Shell.Current.DisplayAlert("Acceso Denegado", "Solo los agentes pueden acceder a esta aplicación.", "OK");
                            return; // Sale del método sin permitir el acceso
                        }

                        if (Preferences.ContainsKey(nameof(App.user)))
                        {
                            Preferences.Remove(nameof(App.user));
                        }

                        string userDetails = JsonConvert.SerializeObject(user);
                        Preferences.Set(nameof(App.user), userDetails);

                        App.user = user;

                        // Navegar a la página principal
                        await Shell.Current.GoToAsync("//MainHome");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Usuario o Contraseña Incorrecta", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Campos Necesarios", "OK");
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
