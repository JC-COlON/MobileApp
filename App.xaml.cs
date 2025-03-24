using DigesettAPP.Models;
using DigesettAPP.Views;

namespace DigesettAPP
{
    public partial class App : Application
    {
        public static User user;
        public App()
        {

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
