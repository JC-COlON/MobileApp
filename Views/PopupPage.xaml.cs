using CommunityToolkit.Maui.Views;  // Aseg�rate de tener este espacio de nombres

namespace DigesettAPP.Views
{
    public partial class PopupPage : Popup
    {
        public PopupPage()
        {
            InitializeComponent();
        }

        // Este m�todo se ejecuta cuando el usuario hace clic en el bot�n "Aceptar"
        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            // Cerrar el pop-up
            this.Close();

            // Despu�s de cerrar, redirigir a la p�gina Home que tiene el TabBar
            await Shell.Current.GoToAsync("//MainHome");
        }

    }
}
