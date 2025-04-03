using DigesettAPP.Service;
using DigesettAPP.ViewModel;


namespace DigesettAPP.Views;



public partial class LoginPage : ContentPage
{
   
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        

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


}
