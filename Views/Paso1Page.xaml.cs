using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace DigesettAPP.Views
{
    public partial class Paso1Page : ContentPage
    {
        public Paso1Page()
        {
            InitializeComponent();

            // Escuchar el mensaje cuando se crea un usuario
            MessagingCenter.Subscribe<CrearUsuarioPage, Usuario>(this, "UsuarioCreado", (sender, usuario) =>
            {
                CedulaEntry.Text = usuario.Cedula;
                NombreEntry.Text = usuario.Name;
                ApellidoEntry.Text = usuario.LastName;
                EmailEntry.Text = usuario.Email;  // Será vacío porque no se captura en la página de registro
                TelefonoEntry.Text = usuario.Phone;

                // Deshabilitar los campos que ya tienen datos, solo dejar habilitados los vacíos
                NombreEntry.IsEnabled = string.IsNullOrEmpty(usuario.Name);
                ApellidoEntry.IsEnabled = string.IsNullOrEmpty(usuario.LastName);
                EmailEntry.IsEnabled = string.IsNullOrEmpty(usuario.Email);
                TelefonoEntry.IsEnabled = string.IsNullOrEmpty(usuario.Phone);

                SiguienteButton.IsEnabled = true; // Habilitar el botón "Siguiente"
            });
        }

        private async void OnCedulaChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 11)
            {
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 11); // Restringe a 11 caracteres
                return;
            }

            if (e.NewTextValue.Length == 11) // Verificamos que la cédula tenga 11 caracteres exactos
            {
                var usuario = await BuscarUsuarioEnBD(e.NewTextValue);
                if (usuario != null)
                {
                    // Llenamos los campos con los datos obtenidos
                    NombreEntry.Text = usuario.Name;
                    ApellidoEntry.Text = usuario.LastName;
                    TelefonoEntry.Text = usuario.Phone;
                    EmailEntry.Text = usuario.Email;

                    // Deshabilitamos los campos que ya tienen datos y habilitamos los vacíos
                    NombreEntry.IsEnabled = string.IsNullOrEmpty(usuario.Name);
                    ApellidoEntry.IsEnabled = string.IsNullOrEmpty(usuario.LastName);
                    TelefonoEntry.IsEnabled = string.IsNullOrEmpty(usuario.Phone);
                    EmailEntry.IsEnabled = string.IsNullOrEmpty(usuario.Email);

                    SiguienteButton.IsEnabled = true; // Habilitar el botón de "Siguiente"
                }
                else
                {
                    bool registrar = await DisplayAlert("Usuario No Registrado", "¿Desea registrar un nuevo usuario?", "Registrar", "Cancelar");

                    if (registrar)
                    {
                        await Task.Delay(100); // Pequeño delay para evitar el "freeze" del alert
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
