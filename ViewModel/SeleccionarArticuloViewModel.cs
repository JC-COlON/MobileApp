using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // <-- Necesario para JObject
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

public partial class SeleccionarArticuloViewModel : ObservableObject
{
    private readonly HttpClient _httpClient = new();

    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            SetProperty(ref searchText, value);
            Search(); // Ejecuta la búsqueda cada vez que se cambia el texto
        }
    }


    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private ObservableCollection<Article> articles = new();

    [ObservableProperty]
    private ObservableCollection<Article> filteredArticles = new();

    public SeleccionarArticuloViewModel()
    {
        LoadArticles();
    }

    [RelayCommand]
    private void Search()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            FilteredArticles = new ObservableCollection<Article>(Articles);
        }
        else
        {
            var lower = SearchText.ToLower();
            FilteredArticles = new ObservableCollection<Article>(
                Articles.Where(a => a.articleNum.ToLower().Contains(lower) || a.description.ToLower().Contains(lower))
            );
        }
    }

    private async void LoadArticles()
    {
        try
        {
            IsLoading = true;

            var response = await _httpClient.GetStringAsync("https://1037-200-215-234-53.ngrok-free.app/api/Article/GetAllArticles");

            // ✅ NUEVO: Deserialización manual con JObject
            var jObject = JsonConvert.DeserializeObject<JObject>(response);
            var articlesArray = jObject["articles"]?["$values"];

            var articlesList = articlesArray?.ToObject<List<Article>>() ?? new List<Article>();

            Articles = new ObservableCollection<Article>(articlesList);
            FilteredArticles = new ObservableCollection<Article>(articlesList);
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
}

public class Article
{
    
        public int articleId { get; set; }
        public string articleNum { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        public string DisplayText => $"{articleNum} - {description}";
    

}

// ❌ Ya no se necesita ApiResponse ni ArticleWrapper, puedes eliminarlos si quieres
