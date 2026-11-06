namespace Web.Services;

public interface IApiClient
{
    ValueTask<List<BookDto>> GetBooksAsync();
    ValueTask<BookDto> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterDto>> GetCharactersAsync();
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
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

    public async ValueTask<List<BookDto>> GetBooksAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/books";
            logger.LogInformation("Fetching books from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<BookDto>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching books");
            throw;
        }
    }

    public async ValueTask<BookDto> GetBookByIdAsync(string bookId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/books/{bookId}";
            logger.LogInformation("Fetching book {BookId} from {Url}", bookId, url);
            var response = await httpClient.GetFromJsonAsync<BookDto>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching book {BookId}", bookId);
            throw;
        }
    }

    public async ValueTask<List<CharacterDto>> GetCharactersAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/characters";
            logger.LogInformation("Fetching characters from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<CharacterDto>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching characters");
            throw;
        }
    }

    public async ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/characters/{characterId}";
            logger.LogInformation("Fetching character {CharacterId} from {Url}", characterId, url);
            var response = await httpClient.GetFromJsonAsync<CharacterDto>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching character {CharacterId}", characterId);
            throw;
        }
    }
}
