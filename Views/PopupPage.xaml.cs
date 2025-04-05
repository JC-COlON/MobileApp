using CommunityToolkit.Maui.Views;  // Asegúrate de tener este espacio de nombres

namespace DigesettAPP.Views
{
    public partial class PopupPage : Popup
    {
        public PopupPage()
        {
            InitializeComponent();
        }

        // Este método se ejecuta cuando el usuario hace clic en el botón "Aceptar"
        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            // Cerrar el pop-up
            this.Close();

            // Después de cerrar, redirigir a la página Home que tiene el TabBar
            await Shell.Current.GoToAsync("//MainHome");
        }

    }
}
