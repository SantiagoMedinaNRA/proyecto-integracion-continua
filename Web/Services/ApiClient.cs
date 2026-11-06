namespace Web.Services;

public interface IApiClient
{
    ValueTask<List<BookResponse>> GetBooksAsync();
    ValueTask<BookResponse> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterResponse>> GetCharactersAsync();
    ValueTask<CharacterResponse> GetCharacterByIdAsync(string characterId);
}

public class ApiClient : IApiClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<ApiClient> logger;
    private readonly ApiSettings apiSettings;

    public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger, IOptions<ApiSettings> options)
    {
        this.httpClient = httpClient;
        this.logger = logger;
        this.apiSettings = options.Value;
    }

    public async ValueTask<List<BookResponse>> GetBooksAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/books";
            logger.LogInformation("Fetching books from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<BookResponse>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching books");
            throw;
        }
    }

    public async ValueTask<BookResponse> GetBookByIdAsync(string bookId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/books/{bookId}";
            logger.LogInformation("Fetching book {BookId} from {Url}", bookId, url);
            var response = await httpClient.GetFromJsonAsync<BookResponse>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching book {BookId}", bookId);
            throw;
        }
    }

    public async ValueTask<List<CharacterResponse>> GetCharactersAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/characters";
            logger.LogInformation("Fetching characters from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<CharacterResponse>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching characters");
            throw;
        }
    }

    public async ValueTask<CharacterResponse> GetCharacterByIdAsync(string characterId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/characters/{characterId}";
            logger.LogInformation("Fetching character {CharacterId} from {Url}", characterId, url);
            var response = await httpClient.GetFromJsonAsync<CharacterResponse>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching character {CharacterId}", characterId);
            throw;
        }
    }
}
