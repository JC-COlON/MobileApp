using CommunityToolkit.Maui.Views;
using DigesettAPP.Models;
using DigesettAPP.ViewModel;

namespace DigesettAPP.Views;

public partial class PoputSeleccionarLugar : Popup
{
    public PoputSeleccionarLugar()
    {
        InitializeComponent();
        BindingContext = new SeleccionarLugarViewModel();
    }

    private void CerrarPopup(object sender, EventArgs e)
    {
        Close();
    }

    //private void OnOfficeTapped(object sender, EventArgs e)
    //{
    //    if (sender is Frame frame && frame.BindingContext is Office office)
    //    {
    //        Close(office); // Devuelve la oficina seleccionada
    //    }
    //}
}
