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


}
