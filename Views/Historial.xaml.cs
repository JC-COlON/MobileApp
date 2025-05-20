using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using DigesettAPP.Models;
using CommunityToolkit.Maui.Views;
using System.Net;

namespace DigesettAPP.Views
{
    public partial class Historial : ContentPage
    {
        private const string BaseUrl = "https://digesett.somee.com/api/Ticket/FilterOrGetTicket?NoAgente=";

        public Historial()
        {
            InitializeComponent();
            TicketsList.ItemTapped += TicketsList_ItemTapped;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            TicketSearchBar.Text = string.Empty;

            CargarHistorial();
        }

        private List<Ticket> allTickets = new List<Ticket>();

        private async void CargarHistorial()
        {
            string noAgente = Preferences.Get("NoAgente", string.Empty);
            if (string.IsNullOrEmpty(noAgente))
            {
                await DisplayAlert("Error", "No se pudo obtener el número de agente.", "OK");
                return;
            }

            string url = $"{BaseUrl}{noAgente}";

            try
            {
                LoadingOverlay.IsVisible = true;

                using (HttpClient client = new HttpClient())
                {
                    string token = Preferences.Get("AuthToken", string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        allTickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);
                        allTickets = allTickets
                            .OrderByDescending(t => DateTime.Parse(t.TicketDate))
                            .ToList();

                        TicketsList.ItemsSource = allTickets;
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Caso especial: no hay multas para este agente
                        TicketsList.ItemsSource = new List<Ticket>();
                        await DisplayAlert("Información", "Este agente no ha puesto ninguna multa.", "OK");
                        await Task.Delay(200);
                        await Shell.Current.GoToAsync("//MainHome");

                    }
                    else
                    {
                        TicketsList.ItemsSource = new List<Ticket>();
                        await DisplayAlert("Error", "No se pudo cargar el historial.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                TicketsList.ItemsSource = new List<Ticket>();
                await DisplayAlert("Error", $"Ocurrió un error inesperado: {ex.Message}", "OK");
            }
            finally
            {
                LoadingOverlay.IsVisible = false;
            }
        }



        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();

            var filteredTickets = allTickets.FindAll(t =>
                (!string.IsNullOrEmpty(t.TicketNumber) && t.TicketNumber.ToLower().Contains(searchText)) ||
                (!string.IsNullOrEmpty(t.LicensePlate) && t.LicensePlate.ToLower().Contains(searchText)) ||
                (!string.IsNullOrEmpty(t.FormattedDate) && t.FormattedDate.ToLower().Contains(searchText)) ||
                (!string.IsNullOrEmpty(t.Name) && t.Name.ToLower().Contains(searchText)) ||
                (!string.IsNullOrEmpty(t.LastName) && t.LastName.ToLower().Contains(searchText)) ||
                (!string.IsNullOrEmpty(t.Brand) && t.Brand.ToLower().Contains(searchText)) ||
                (!string.IsNullOrEmpty(t.Model) && t.Model.ToLower().Contains(searchText))
            );

            TicketsList.ItemsSource = filteredTickets;
        }


        private async void TicketsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Ticket selectedTicket)
            {
                var popup = new PopupDetalleMulta(selectedTicket);
                this.ShowPopup(popup);
            }

    // Deseleccionar el ítem para que pueda volver a tocarse después
    ((ListView)sender).SelectedItem = null;
        }


    }
}
