using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using DigesettAPP.Models;

namespace DigesettAPP.Views
{
    public partial class Historial : ContentPage
    {
        private const string BaseUrl = "https://digesett.somee.com/api/Ticket/FilterOrGetTicket?NoAgente=";

        public Historial()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarHistorial();
        }

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
                // 👀 Mostrar loading
                LoadingOverlay.IsVisible = true;

                using (HttpClient client = new HttpClient())
                {
                    string token = Preferences.Get("AuthToken", string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    TicketsList.ItemsSource = null;
                    TicketsList.ItemsSource = new List<Ticket>();

                    HttpResponseMessage response = await client.GetAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);

                        if (tickets != null && tickets.Count > 0)
                        {
                            TicketsList.ItemsSource = tickets;
                        }
                        else
                        {
                            await DisplayAlert("Historial vacío", "No tienes multas registradas aún.", "OK");
                            await Shell.Current.GoToAsync("//MainHome");
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        await DisplayAlert("Historial vacío", "No tienes multas registradas aún.", "OK");
                        await Shell.Current.GoToAsync("//MainHome");
                    }
                    else
                    {
                        TicketsList.ItemsSource = new List<Ticket>();
                        await DisplayAlert("Error", $"No se pudo cargar el historial.\nCódigo: {response.StatusCode}\nMensaje: {jsonResponse}", "OK");
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
                // ✅ Ocultar loading
                LoadingOverlay.IsVisible = false;
            }
        }



    }
}
