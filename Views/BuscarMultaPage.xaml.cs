using Newtonsoft.Json;

namespace DigesettAPP.Views;


    public partial class BuscarMultaPage : ContentPage
    {
        public BuscarMultaPage()
        {
            InitializeComponent();
        }

        private async void CedulaEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 11)
            {
                await BuscarMultasPorCedula(e.NewTextValue);
            }
        }

    private async Task BuscarMultasPorCedula(string cedula)
    {
        try
        {
            string url = $"https://digesett.somee.com/api/Ticket/FilterOrGetTicket?Cedula={cedula}&Estado=pending&Hidden=0";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Si el código es 404 y el mensaje indica que no hay resultados, lo manejamos como caso válido
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound && responseBody.Contains("No se encontraron multas"))
                {
                    await DisplayAlert("Sin resultados", "No se encontraron multas para esta cédula.", "OK");
                    MultasCollectionView.ItemsSource = null;
                    return;
                }

                // Si es otro tipo de error, lo mostramos como error
                await DisplayAlert("Error", $"Error al buscar las multas. Código: {response.StatusCode}", "OK");
                return;
            }

            var result = JsonConvert.DeserializeObject<List<MultaApiData>>(responseBody);

            if (result != null && result.Count > 0)
            {
                var multas = result.Select(x => new MultaResultado
                {
                    Fecha = DateTime.Parse(x.ticketDate).ToString("dd/MM/yyyy"),
                    Articulo = $"{x.articulo.articleNum} - {x.articulo.description}",
                    Zona = x.zone
                }).ToList();

                MultasCollectionView.ItemsSource = multas;
            }
            else
            {
                await DisplayAlert("Sin resultados", "No se encontraron multas para esta cédula.", "OK");
                MultasCollectionView.ItemsSource = null;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al buscar las multas:\n{ex.Message}", "OK");
        }
    }




    // Clases de ayuda
    public class ApiResponse
    {
        [JsonProperty("$values")]
        public List<MultaApiData> Values { get; set; }
    }


    public class MultaApiData
        {
            public string ticketDate { get; set; }
            public string incidentLocation { get; set; }
            public string zone { get; set; }
            public Articulo articulo { get; set; }
        }

        public class Articulo
        {
            public string articleNum { get; set; }
            public string description { get; set; }
        }

        public class MultaResultado
        {
            public string Fecha { get; set; }
            public string Articulo { get; set; }
            public string Zona { get; set; }
        }
    }