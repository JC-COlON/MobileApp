using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace DigesettAPP.Views
{
    public partial class Paso1Page : ContentPage
    {
        public Paso1Page()
        {
            InitializeComponent();

            // Cargar datos guardados si existen
            CargarDatosGuardados();

            // Escuchar el mensaje cuando se crea un usuario
            MessagingCenter.Subscribe<CrearUsuarioPage, Usuario>(this, "UsuarioCreado", (sender, usuario) =>
            {
                CedulaEntry.Text = usuario.Cedula;
                NombreEntry.Text = usuario.Name;
                ApellidoEntry.Text = usuario.LastName;
                EmailEntry.Text = usuario.Email;
                TelefonoEntry.Text = usuario.Phone;

                NombreEntry.IsEnabled = string.IsNullOrEmpty(usuario.Name);
                ApellidoEntry.IsEnabled = string.IsNullOrEmpty(usuario.LastName);
                EmailEntry.IsEnabled = string.IsNullOrEmpty(usuario.Email);
                TelefonoEntry.IsEnabled = string.IsNullOrEmpty(usuario.Phone);

                SiguienteButton.IsEnabled = true;
                GuardarDatosTemporalmente(usuario);
            });
        }

        private async void OnCedulaChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 11)
            {
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 11);
                return;
            }

            if (e.NewTextValue.Length == 11)
            {
                var usuario = await BuscarUsuarioEnBD(e.NewTextValue);
                if (usuario != null)
                {
                    NombreEntry.Text = usuario.Name;
                    ApellidoEntry.Text = usuario.LastName;
                    TelefonoEntry.Text = usuario.Phone;
                    EmailEntry.Text = usuario.Email;

                    NombreEntry.IsEnabled = string.IsNullOrEmpty(usuario.Name);
                    ApellidoEntry.IsEnabled = string.IsNullOrEmpty(usuario.LastName);
                    TelefonoEntry.IsEnabled = string.IsNullOrEmpty(usuario.Phone);
                    EmailEntry.IsEnabled = string.IsNullOrEmpty(usuario.Email);

                    SiguienteButton.IsEnabled = true;
                    GuardarDatosTemporalmente(usuario);
                }
                else
                {
                    bool registrar = await DisplayAlert("Usuario No Registrado", "¿Desea registrar un nuevo usuario?", "Registrar", "Cancelar");
                    if (registrar)
                    {
                        await Task.Delay(100);
                        await Navigation.PushAsync(new CrearUsuarioPage());
                    }
                }
            }
        }

        private async Task<Usuario> BuscarUsuarioEnBD(string cedula)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://digesett.somee.com/api/User/{cedula}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Usuario>(json);
                }
            }
            return null;
        }

        private async void IrPaso2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Paso2Page());
        }

        private async void IrAtras(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void GuardarDatosTemporalmente(Usuario usuario)
        {
            Preferences.Set("Cedula", usuario.Cedula);
            Preferences.Set("Nombre", usuario.Name);
            Preferences.Set("Apellido", usuario.LastName);
            Preferences.Set("Telefono", usuario.Phone);
            Preferences.Set("Email", usuario.Email);
        }

        private void CargarDatosGuardados()
        {
            CedulaEntry.Text = Preferences.Get("Cedula", string.Empty);
            NombreEntry.Text = Preferences.Get("Nombre", string.Empty);
            ApellidoEntry.Text = Preferences.Get("Apellido", string.Empty);
            TelefonoEntry.Text = Preferences.Get("Telefono", string.Empty);
            EmailEntry.Text = Preferences.Get("Email", string.Empty);

            SiguienteButton.IsEnabled = !string.IsNullOrEmpty(CedulaEntry.Text);
        }

        public static void LimpiarDatosTemporales()
        {
            Preferences.Remove("Cedula");
            Preferences.Remove("Nombre");
            Preferences.Remove("Apellido");
            Preferences.Remove("Telefono");
            Preferences.Remove("Email");
        }
    }

    public class Usuario
    {
        public string Cedula { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
