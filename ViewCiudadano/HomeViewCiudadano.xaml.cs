using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewModels;
using DigesettAPP.Views;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Core.Carousel;
using System;
using System.Threading.Tasks;

namespace DigesettAPP.ViewCiudadano
{
    public partial class HomeViewCiudadano : ContentPage
    {
        private HomeViewModel viewModel;
        public HomeViewCiudadano()
        {
            InitializeComponent();

            viewModel = new HomeViewModel();
            BindingContext = viewModel;
            carouselView.IndicatorView = indicatorview;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadMultasPendientes(); // ✅ ahora lo puedes esperar!
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

        private async void AbrirNoticia_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton && imageButton.CommandParameter is string url)
            {
                try
                {
                    await Launcher.Default.OpenAsync(url);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo abrir la noticia: {ex.Message}", "OK");
                }
            }
        }

    }
}
