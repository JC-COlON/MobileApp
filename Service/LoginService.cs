using System.Net.Http;
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
                //string Url = "https://localhost:7277/api/UserAccess/Login"; // Ajusta la URL si es 
                string Url = "https://digesett.somee.com/api/UserAccess/Login";
                //string Url = "https://192.168.100.56:7277/api/UserAccess/Login";
                //string Url = "https://10.0.2.2:7277/api/UserAccess/Login"; // URL para el emulador

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

        //public async Task<User> Login(string cedula, string password)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        //string Url = "https://localhost:7277/api/UserAccess/Login"; // Ajusta la URL si es necesario
        //        string Url = "https://192.168.100.56:7277/api/UserAccess/Login"; // URL para el emulador
        //        //string Url = "https://10.0.2.2:7277/api/UserAccess/Login"; // URL para el emulador

        //        // Crear el objeto que se enviará en el cuerpo de la solicitud POST
        //        var loginData = new { Cedula = cedula, Password = password };

        //        // Serializar el objeto a JSON
        //        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

        //        // Hacer la solicitud POST
        //        HttpResponseMessage response = await client.PostAsync(Url, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Deserializar la respuesta en un objeto User
        //            User user = await response.Content.ReadFromJsonAsync<User>();
        //            return user;
        //        }
        //        return null; // En caso de error
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones
        //        Console.WriteLine($"Error en la solicitud: {ex.Message}");
        //        return null;
        //    }
        //}


    }
}











