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
    }
}
