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
    // Mostrar popup
    public void MostrarPopup()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            popup.Show();
        });
    }

    // Ocultar popup
    public void OcultarPopup()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (popup.IsOpen)
                popup.Dismiss();
        });
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
