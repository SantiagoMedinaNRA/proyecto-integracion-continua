namespace Web.Services;

public interface IApiClient
{
    ValueTask<List<BookDto>> GetBooksAsync();
    ValueTask<BookDto> GetBookByIdAsync(string bookId);
    ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20);
    ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId);
    ValueTask<List<SpellDto>> GetSpellsAsync();
    ValueTask<SpellDto> GetSpellByIdAsync(string spellId);
    ValueTask<List<PotionDto>> GetPotionsAsync();
    ValueTask<PotionDto> GetPotionByIdAsync(string potionId);
    ValueTask<List<MovieDto>> GetMoviesAsync();
    ValueTask<MovieDto> GetMovieByIdAsync(string movieId);
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

    public async ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/characters?pageNumber={pageNumber}&pageSize={pageSize}";
            logger.LogInformation("Fetching characters from {Url} (page {PageNumber}, size {PageSize})", url, pageNumber, pageSize);
            var response = await httpClient.GetFromJsonAsync<List<CharacterDto>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching characters (page {PageNumber})", pageNumber);
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

    public async ValueTask<List<SpellDto>> GetSpellsAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/spells";
            logger.LogInformation("Fetching spells from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<SpellDto>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching spells");
            throw;
        }
    }

    public async ValueTask<SpellDto> GetSpellByIdAsync(string spellId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/spells/{spellId}";
            logger.LogInformation("Fetching spell {SpellId} from {Url}", spellId, url);
            var response = await httpClient.GetFromJsonAsync<SpellDto>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching spell {SpellId}", spellId);
            throw;
        }
    }

    public async ValueTask<List<PotionDto>> GetPotionsAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/potions";
            logger.LogInformation("Fetching potions from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<PotionDto>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching potions");
            throw;
        }
    }

    public async ValueTask<PotionDto> GetPotionByIdAsync(string potionId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/potions/{potionId}";
            logger.LogInformation("Fetching potion {PotionId} from {Url}", potionId, url);
            var response = await httpClient.GetFromJsonAsync<PotionDto>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching potion {PotionId}", potionId);
            throw;
        }
    }

    public async ValueTask<List<MovieDto>> GetMoviesAsync()
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/movies";
            logger.LogInformation("Fetching movies from {Url}", url);
            var response = await httpClient.GetFromJsonAsync<List<MovieDto>>(url);
            return response ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching movies");
            throw;
        }
    }

    public async ValueTask<MovieDto> GetMovieByIdAsync(string movieId)
    {
        try
        {
            string url = $"{apiSettings.BaseUrl}/api/movies/{movieId}";
            logger.LogInformation("Fetching movie {MovieId} from {Url}", movieId, url);
            var response = await httpClient.GetFromJsonAsync<MovieDto>(url);
            return response!;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching movie {MovieId}", movieId);
            throw;
        }
    }
}
