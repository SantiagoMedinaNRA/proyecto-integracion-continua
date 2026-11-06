namespace Api.Brokers;

public class PotterDbBroker : IPotterDbBroker
{
    private readonly HttpClient httpClient;
    private readonly ILogger<PotterDbBroker> logger;
    private readonly IConfiguration configuration;

    public PotterDbBroker(HttpClient httpClient, ILogger<PotterDbBroker> logger, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        this.logger = logger;
        this.configuration = configuration;
    }

    public async ValueTask<List<BookResponse>> GetBooksAsync()
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.BooksEndpoint}";
            logger.LogInformation("Fetching books from {Url}", url);

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var books = JsonSerializer.Deserialize<List<BookResponse>>(
                jsonDocument.RootElement.GetProperty("data").ToString(),
                options) ?? [];

            return books;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching books from PotterDB");
            throw;
        }
    }

    public async ValueTask<BookResponse> GetBookByIdAsync(string bookId)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.BooksEndpoint}/{bookId}";
            logger.LogInformation("Fetching book {BookId} from {Url}", bookId, url);

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var book = JsonSerializer.Deserialize<BookResponse>(
                jsonDocument.RootElement.GetProperty("data").ToString(),
                options)!;

            return book;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching book {BookId} from PotterDB", bookId);
            throw;
        }
    }
}
