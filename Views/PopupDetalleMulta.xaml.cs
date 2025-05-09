using CommunityToolkit.Maui.Views;
using DigesettAPP.Models;

namespace DigesettAPP.Views
{
    public partial class PopupDetalleMulta : Popup
    {
        public PopupDetalleMulta(Ticket ticket)
        {
            InitializeComponent();
            BindingContext = ticket;
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
