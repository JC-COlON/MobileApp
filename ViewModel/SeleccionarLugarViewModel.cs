using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigesettAPP.ViewModel
{

    public partial class SeleccionarLugarViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient = new();

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                Search();
            }
        }

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private ObservableCollection<IncidentLocation> locations = new();

        [ObservableProperty]
        private ObservableCollection<IncidentLocation> filteredLocation = new();

        public SeleccionarLugarViewModel()
        {
            LoadLocation();
        }

        private async void LoadLocation()
        {
            try
            {
                IsLoading = true;

                var response = await _httpClient.GetStringAsync("https://5fce-200-215-234-53.ngrok-free.app/api/Ticket/alllocation");

                var jObject = JsonConvert.DeserializeObject<JObject>(response);
                var officesArray = jObject["$values"];

                var officeList = officesArray?.ToObject<List<IncidentLocation>>() ?? new List<IncidentLocation>();

                Locations = new ObservableCollection<IncidentLocation>(officeList);
                FilteredLocation = new ObservableCollection<IncidentLocation>(officeList);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredLocation = new ObservableCollection<IncidentLocation>(Locations);
            }
            else
            {
                var lower = SearchText.ToLower();
                FilteredLocation = new ObservableCollection<IncidentLocation>(
                    Locations.Where(o => o.Name.ToLower().Contains(lower))
                );
            }
        }

    }
}
public class IncidentLocation
{
    [JsonProperty("officeId")]
    public int IncidentLocationId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    public string DisplayText => Name;
}

