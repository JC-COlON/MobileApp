using CommunityToolkit.Maui.Views;
using DigesettAPP.Service;
using DigesettAPP.ViewCiudadano;
using DigesettAPP.ViewModel;
using Microsoft.Win32;


namespace DigesettAPP.Views;



public partial class LoginPage : ContentPage
{

    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        vm.LoginPage = this; // <-- ESTO ES LO QUE FALTA
        BindingContext = vm;
    }


    // Mostrar popup
    public void MostrarPopup()
    {
        popup.Show();
    }

    // Ocultar popup
    public void OcultarPopup()
    {
        popup.Dismiss();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is LoginViewModel vm)
        {
            vm.LimpiarCampos();
        }
    }


    //private async void OnForgotPasswordTapped(object sender, EventArgs e)
    //{
    //    await Shell.Current.GoToAsync("//RecuperarContrasenaPage");
    //}


    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OlvidelaContrasenaPage());
    }


    private async void GoToRegister(object sender, TappedEventArgs e)
    {


        await Shell.Current.GoToAsync("RegistrarViewCiudadano");



    }


}
