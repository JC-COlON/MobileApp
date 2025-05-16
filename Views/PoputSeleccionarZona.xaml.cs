using CommunityToolkit.Maui.Views;
using DigesettAPP.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace DigesettAPP.Views
{
    public partial class PoputSeleccionarZona : Popup
    {
        public PoputSeleccionarZona()
        {
            InitializeComponent();
            BindingContext = new SeleccionarZonaViewModel();
        }

        private void CerrarPopup(object sender, EventArgs e)
        {
            Close(); // Cierra sin devolver datos
        }

        private void OficinaSeleccionada(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is LocationModel oficina)
            {
                Close(oficina); // Cierra devolviendo la oficina seleccionada
            }
        }
    }
}
