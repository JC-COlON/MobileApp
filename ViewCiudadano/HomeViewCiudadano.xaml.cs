using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewModels;
using DigesettAPP.Views;
using System;
using System.Threading.Tasks;

namespace DigesettAPP.ViewCiudadano
{
    public partial class HomeViewCiudadano : ContentPage
    {
        public HomeViewCiudadano()
        {
            InitializeComponent();

            // Establece el ViewModel como contexto de enlace
            BindingContext = new HomeViewModel();
        }

        private async void IrPerfilCiudadano(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PerfilPageCiudadano));
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var frame2 = (Frame)sender;

            // Animación combinada de opacidad y escala
            await Task.WhenAll(
                frame2.ScaleTo(0.95, 100, Easing.CubicOut),
                frame2.FadeTo(0.8, 100)
            );

            await Task.WhenAll(
                frame2.ScaleTo(1, 100, Easing.CubicIn),
                frame2.FadeTo(1, 100)
            );

            await Shell.Current.GoToAsync(nameof(ListadoTarjetasCiudadano));
        }




        private async void IrAverMultass(object sender, TappedEventArgs e)
        {
            var border = sender as Border;  // Ahora estamos trabajando con Border

            // Verificar si el cast fue exitoso
            if (border != null)
            {
                // Animación combinada de opacidad y escala
                await Task.WhenAll(
                    border.ScaleTo(0.95, 100, Easing.CubicOut),
                    border.FadeTo(0.8, 100)
                );

                await Task.WhenAll(
                    border.ScaleTo(1, 100, Easing.CubicIn),
                    border.FadeTo(1, 100)
                );

                // Navegar a la siguiente página
                await Shell.Current.GoToAsync(nameof(ListaMultasCiudadano));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("El sender no es un Border");
            }
        }


        private async void iraMultapagar(object sender, TappedEventArgs e)
        {
            var frame2 = (Frame)sender;

            // Animación combinada de opacidad y escala
            await Task.WhenAll(
                frame2.ScaleTo(0.95, 100, Easing.CubicOut),
                frame2.FadeTo(0.8, 100)
            );

            await Task.WhenAll(
                frame2.ScaleTo(1, 100, Easing.CubicIn),
                frame2.FadeTo(1, 100)
            );

            await Shell.Current.GoToAsync(nameof(ListaMultasParaPagar));
        }

    }
}
