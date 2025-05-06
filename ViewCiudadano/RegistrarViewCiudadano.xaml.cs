using DigesettAPP.ViewModels;

namespace DigesettAPP.ViewCiudadano
{
    public partial class RegistrarViewCiudadano : ContentPage
    {
        public RegistrarViewCiudadano()
        {
            InitializeComponent();
            BindingContext = new RegistrarCiudadanoModel(); // <-- Enlaza el ViewModel
        }

        private async void GoToRegister(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }



    }
}
