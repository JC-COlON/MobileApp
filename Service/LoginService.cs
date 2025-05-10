using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DigesettAPP.Models;
using Newtonsoft.Json;

namespace DigesettAPP.Service
{
    public class LoginService : ILoginRepository
    {

        public async Task<User> Login(string cedula, string password)
        {
            try
            {
                var client = new HttpClient();
                string Url = "https://e359-38-158-200-68.ngrok-free.app/api/UserAccess/Login";

                var loginData = new { Cedula = cedula, Password = password };
                var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Url, content);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (loginResponse != null && loginResponse.isSuccess && loginResponse.user != null)
                    {
                        // Guardar el token
                        Preferences.Set("AuthToken", loginResponse.token);

                        // ✅ Obtener y guardar NoAgente usando la cédula
                        await ObtenerYGuardarNoAgente(cedula);

                        return loginResponse.user;
                    }
                }

                return null; // Usuario incorrecto o error en la API
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚨 Error inesperado: {ex.Message}");
                return null;
            }
        }


        private async Task ObtenerYGuardarNoAgente(string cedula)
        {
            try
            {
                string token = Preferences.Get("AuthToken", string.Empty);
                if (string.IsNullOrEmpty(token)) return;

                string url = $"https://e359-38-158-200-68.ngrok-free.app/api/User/{cedula}";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var usuario = JsonConvert.DeserializeObject<UsuarioResponse>(json);

                        if (usuario != null && !string.IsNullOrEmpty(usuario.NoAgente))
                        {
                            Preferences.Set("NoAgente", usuario.NoAgente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener NoAgente: {ex.Message}");
            }
        }



    }
}











