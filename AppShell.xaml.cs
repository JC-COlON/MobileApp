using DigesettAPP.ViewCiudadano;
using DigesettAPP.Views;

namespace DigesettAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(Paso1Page), typeof(Paso1Page));
            Routing.RegisterRoute(nameof(Paso2Page), typeof(Paso2Page));
            Routing.RegisterRoute(nameof(Paso3Page), typeof(Paso3Page));
            Routing.RegisterRoute(nameof(CrearUsuarioPage), typeof(CrearUsuarioPage));
            Routing.RegisterRoute(nameof(BuscarMultaPage), typeof(BuscarMultaPage));
            Routing.RegisterRoute(nameof(PerfilPageCiudadano), typeof(PerfilPageCiudadano));
            Routing.RegisterRoute(nameof(RegistrarViewCiudadano), typeof(RegistrarViewCiudadano));
            Routing.RegisterRoute(nameof(AgregarTarjetaCiudadano), typeof(AgregarTarjetaCiudadano));
            Routing.RegisterRoute(nameof(PagarMultaViewCiudadano), typeof(PagarMultaViewCiudadano));
            Routing.RegisterRoute(nameof(MultaNotificacionCiudadano), typeof(MultaNotificacionCiudadano));
            Routing.RegisterRoute(nameof(ListaMultasCiudadano), typeof(ListaMultasCiudadano));
            Routing.RegisterRoute(nameof(ListaMultasParaPagar), typeof(ListaMultasParaPagar));
            Routing.RegisterRoute(nameof(ListadoTarjetasCiudadano), typeof(ListadoTarjetasCiudadano));
            Routing.RegisterRoute(nameof(PagarMultaViewCiudadano), typeof(PagarMultaViewCiudadano));




        }
    }
}
