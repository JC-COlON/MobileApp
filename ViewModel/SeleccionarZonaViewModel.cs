using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace DigesettAPP.ViewModel
{
    public partial class SeleccionarZonaViewModel : ObservableObject
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
        private ObservableCollection<ZoneModel> zones = new();

        [ObservableProperty]
        private ObservableCollection<ZoneModel> filteredZones = new();

        public SeleccionarZonaViewModel()
        {
            LoadZones();
        }

        private async void LoadZones()
        {
            try
            {
                IsLoading = true;

                var response = await _httpClient.GetStringAsync("https://digesett.somee.com/api/Ticket/zoneWithLocation");

                var jObject = JsonConvert.DeserializeObject<JObject>(response);
                var zonesArray = jObject["$values"];

                var zoneList = zonesArray?.ToObject<List<ZoneModel>>() ?? new List<ZoneModel>();

                Zones = new ObservableCollection<ZoneModel>(zoneList);
                FilteredZones = new ObservableCollection<ZoneModel>(zoneList);
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
                FilteredZones = new ObservableCollection<ZoneModel>(zones);
            }
            else
            {
                var lower = SearchText.ToLower();
                FilteredZones = new ObservableCollection<ZoneModel>(
                    zones.Where(z => z.Name.ToLower().Contains(lower))
                );
            }
        }

        public ICommand ToggleExpandCommand => new Command<ZoneModel>((zone) =>
        {
            if (zone != null)
            {
                zone.IsExpanded = !zone.IsExpanded;
            }
        });
    }

    public class LocationModel
    {
        [JsonProperty("officeId")]
        public int LocationId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class ZoneModel : ObservableObject
    {
        [JsonProperty("zoneId")]
        public int ZoneId { get; set; }

        [JsonProperty("zoneName")]
        public string Name { get; set; }

        [JsonProperty("offices")]
        public List<LocationModel> Locations { get; set; }

        public string DisplayText => Name;

        [ObservableProperty]
        private bool isExpanded;
    }
}
