using CommunityToolkit.Maui.Views;
using DigesettAPP.Models;
using DigesettAPP.ViewModel;

namespace DigesettAPP.ViewCiudadano;

public partial class PoputSeleccionarArticulo : Popup
{
    public PoputSeleccionarArticulo()
    {
        InitializeComponent();
        BindingContext = new SeleccionarArticuloViewModel();
    }

    // ✅ Cierra el popup sin devolver nada
    private void CerrarPopup(object sender, EventArgs e)
    {
        Close();
    }

    // ✅ Cierra el popup y devuelve el artículo seleccionado
    private void OnArticleTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Article article)
        {
            Close(article); // Devuelve el artículo como resultado
        }
    }
}
