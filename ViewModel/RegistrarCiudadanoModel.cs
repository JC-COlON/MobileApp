using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Text.RegularExpressions;



namespace DigesettAPP.ViewModels
{
    public class RegistrarCiudadanoModel : BindableObject
    {
        private readonly HttpClient _httpClient = new();

        private string _cedula;
        public string Cedula
        {
            get => _cedula;
            set
            {
                _cedula = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }



        private string _rawPhone = "";
        public string RawPhone
        {
            get => _rawPhone;
            set
            {
                if (_rawPhone != value)
                {
                    _rawPhone = value;

                    // Notificar cambio de RawPhone (por si lo usas en otro lado)
                    OnPropertyChanged(nameof(RawPhone));

                    // Notificar cambio de Phone (el formateado)
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OnPropertyChanged(nameof(Phone));
                    });
                }
            }
        }

        public string Phone
        {
            get
            {
                if (!IsNewUser && !string.IsNullOrWhiteSpace(_rawPhone) && _rawPhone.Length == 10)
                {
                    return $"***-***-**{_rawPhone.Substring(8)}";
                }
                return _rawPhone;
            }
            set
            {
                if (IsNewUser)
                {
                    _rawPhone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        private string _confirmPhone;
        public string ConfirmPhone
        {
            get => _confirmPhone;
            set
            {
                _confirmPhone = value;
                OnPropertyChanged();
            }
        }





        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }


        public string ProfileImg { get => _profileImg; set { _profileImg = value; OnPropertyChanged(); } }
        public int? NacionalityId { get => _nacionalityId; set { _nacionalityId = value; OnPropertyChanged(); } }
        public DateTime? Birthdate { get => _birthdate; set { _birthdate = value; OnPropertyChanged(); } }
        public int? GenderId { get => _genderId; set { _genderId = value; OnPropertyChanged(); } }
        public int? BloodTypeId { get => _bloodTypeId; set { _bloodTypeId = value; OnPropertyChanged(); } }
        public decimal? Height { get => _height; set { _height = value; OnPropertyChanged(); } }
        public int? WorkLocationId { get => _workLocationId; set { _workLocationId = value; OnPropertyChanged(); } }
        public int? CivilStatusId { get => _civilStatusId; set { _civilStatusId = value; OnPropertyChanged(); } }

        private string _profileImg = "";
        private int? _nacionalityId;
        private DateTime? _birthdate;
        private int? _genderId;
        private int? _bloodTypeId;
        private decimal? _height;
        private int? _workLocationId;
        private int? _civilStatusId;


        private bool _isNewUser = true;
        public bool IsNewUser
        {
            get => _isNewUser;
            set { _isNewUser = value; OnPropertyChanged(); }
        }

        private bool _showPhoneConfirmation;
        public bool ShowPhoneConfirmation
        {
            get => _showPhoneConfirmation;
            set { _showPhoneConfirmation = value; OnPropertyChanged(); }
        }

        public ICommand ValidateCedulaCommand { get; }
        public ICommand RegisterCommand { get; }



        public RegistrarCiudadanoModel()
        {
            ValidateCedulaCommand = new Command<string>(async (text) => await OnCedulaChangedAsync(text));
            RegisterCommand = new Command(async () => await RegistrarCiudadanoAsync());
        }

        // Método para validar la cédula y cargar/prellenar datos del ciudadano
        private async Task OnCedulaChangedAsync(string cedulaTexto)
        {
            Cedula = cedulaTexto;

            if (Cedula?.Length != 11)
                return;

            IsLoading = true; // Mostrar overlay de carga

            try
            {
                var ciudadano = await BuscarCiudadanoEnBackend(Cedula);
                if (ciudadano != null)
                {
                    // Usuario ya registrado
                    Name = ciudadano.Name;
                    LastName = ciudadano.LastName;

                    RawPhone = ciudadano.Phone;
                    RawPhone = FormatearTelefono(RawPhone);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OnPropertyChanged(nameof(Phone));
                    });

                    Email = ciudadano.Email;
                    GenderId = ciudadano.GenderId ?? 0;
                    ProfileImg = ciudadano.ProfileImg ?? "";
                    NacionalityId = ciudadano.NacionalityId;
                    Birthdate = ciudadano.Birthdate;
                    BloodTypeId = ciudadano.BloodTypeId;
                    Height = ciudadano.Height;
                    WorkLocationId = ciudadano.WorkLocationId;
                    CivilStatusId = ciudadano.CivilStatusId;

                    IsNewUser = false;
                    ShowPhoneConfirmation = true;
                }
                else
                {
                    // Validar con la API externa
                    var validado = await ValidarCedulaExterna(Cedula);
                    if (!validado)
                    {
                        await Shell.Current.DisplayAlert("Cédula inválida", "La cédula no es válida según el padrón.", "OK");
                        return;
                    }

                    // Cédula válida pero nuevo usuario: limpiar datos
                    Name = "";
                    LastName = "";
                    RawPhone = "";
                    OnPropertyChanged(nameof(Phone));

                    Email = "";
                    GenderId = 0;
                    ProfileImg = "";
                    NacionalityId = null;
                    Birthdate = null;
                    BloodTypeId = null;
                    Height = null;
                    WorkLocationId = null;
                    CivilStatusId = null;

                    IsNewUser = true;
                    ShowPhoneConfirmation = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false; // Ocultar overlay de carga
            }
        }


        // Validación externa de cédula
        private async Task<bool> ValidarCedulaExterna(string cedula)
        {
            try
            {
                var url = $"https://api.digital.gob.do/v3/cedulas/{cedula}/validate";
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ValidacionCedulaResponse>(content);
                return result?.Valid ?? false;
            }
            catch
            {
                return false;
            }
        }

        // Buscar ciudadano en tu backend
        private async Task<UsuarioExistenteDto> BuscarCiudadanoEnBackend(string cedula)
        {
            try
            {
                var url = $"https://digesett.somee.com/api/User/{cedula}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UsuarioExistenteDto>(json);
            }
            catch
            {
                return null;
            }
        }

        // Registrar o actualizar al ciudadano
        private async Task RegistrarCiudadanoAsync()
        {
            if (string.IsNullOrWhiteSpace(Cedula) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Phone))
            {
                await Shell.Current.DisplayAlert("Datos requeridos", "Por favor, complete todos los campos obligatorios.", "OK");
                return;
            }

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!emailRegex.IsMatch(Email))
            {
                await Shell.Current.DisplayAlert("Email inválido", "Ingrese un email válido en el formato ejemplo@dominio.com", "OK");
                return;
            }


            if (Password.Length < 6)
            {
                await Shell.Current.DisplayAlert("Contraseña débil", "La contraseña debe tener al menos 6 caracteres.", "OK");
                return;
            }

            if (Phone.Length != 10)
            {
                await Shell.Current.DisplayAlert("Teléfono inválido", "Ingrese un teléfono válido.", "OK");
                return;
            }

            if (!string.IsNullOrWhiteSpace(ConfirmPhone) && _rawPhone != ConfirmPhone)
            {
                await Shell.Current.DisplayAlert("Teléfono no coincide", "El número de teléfono confirmado no coincide con el ingresado.", "OK");
                return;
            }

            IsLoading = true;

            var genderIdToSend = GenderId == 0 ? (int?)null : GenderId;

            var usuario = new
            {
                cedula = Cedula,
                name = Name,
                lastname = LastName,
                password = Password,
                rolId = 3,
                email = Email,
                phone = _rawPhone,
                profileImg = ProfileImg ?? "",
                nacionalityId = NacionalityId,
                birthdate = Birthdate,
                genderId = genderIdToSend,
                bloodTypeId = BloodTypeId,
                height = Height,
                workLocationId = WorkLocationId,
                civilStatusId = CivilStatusId,
                noAgente = "",
                statusId = 1
            };

            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(usuario, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

                // Copiar JSON al portapapeles
                await Clipboard.SetTextAsync(json);

                // Mostrar alerta indicando que se copió
                await Shell.Current.DisplayAlert("JSON copiado", "El JSON a enviar ha sido copiado al portapapeles. A continuación se muestra:", "OK");

                // Mostrar el JSON (puede que se corte si es muy largo)
                await Shell.Current.DisplayAlert("Contenido JSON", json, "OK");

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                if (IsNewUser)
                {
                    response = await _httpClient.PostAsync("https://digesett.somee.com/api/UserAccess/Register", content);
                }
                else
                {
                    response = await _httpClient.PutAsync($"https://digesett.somee.com/api/User/UpdateCiudadano/{Cedula}", content);
                }

                if (response.IsSuccessStatusCode)
                {
                    string mensaje = IsNewUser ? "Usuario registrado correctamente." : "Usuario actualizado correctamente.";
                    await Shell.Current.DisplayAlert("¡Éxito!", mensaje, "OK");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    await Shell.Current.DisplayAlert("Error", error, "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }



        public class ValidacionCedulaResponse
        {
            [JsonProperty("valid")]
            public bool Valid { get; set; }
        }

        private string FormatearTelefono(string telefono)
        {
            if (string.IsNullOrEmpty(telefono) || telefono.Length < 10)
                return "";

            // Tomar los últimos 2 dígitos visibles y el resto enmascarado
            return $"***-***-**{telefono[^2..]}";
        }






        public class UsuarioExistenteDto
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string ProfileImg { get; set; }
            public int? NacionalityId { get; set; }
            public DateTime? Birthdate { get; set; }
            public int? GenderId { get; set; }
            public int? BloodTypeId { get; set; }
            public decimal? Height { get; set; }
            public int? WorkLocationId { get; set; }
            public int? CivilStatusId { get; set; }
        }

    }
}
