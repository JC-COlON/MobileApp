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
                using (HttpClient client = new HttpClient())
                {
                    string token = Preferences.Get("AuthToken", string.Empty);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        
                        var tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);

                        if (tickets != null && tickets.Count > 0)
                        {
                            foreach (var ticket in tickets)
                            {
                                if (!string.IsNullOrEmpty(ticket.TicketDate))
                                {
                                    ticket.TicketDate = DateTime.TryParse(ticket.TicketDate, out DateTime date)
                                        ? date.ToString("dd/MM/yyyy")
                                        : ticket.TicketDate;
                                }

                                ticket.FormattedInfo = $"Ticket # {ticket.TicketId}\n{ticket.Name} {ticket.LastName} - {ticket.LicensePlate} - {ticket.Brand}/{ticket.Model} - {ticket.TicketDate}";
                            }

                            TicketsList.ItemsSource = tickets;
                        }
                        else
                        {
                            await DisplayAlert("Historial vacío", "No tienes multas registradas aún.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo cargar el historial.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

    }
}
