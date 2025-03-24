using DigesettAPP.Views;

namespace DigesettAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));


        }
    }
}
