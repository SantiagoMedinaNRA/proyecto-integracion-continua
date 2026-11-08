namespace Api.Brokers;

public class PotterDbBroker : IPotterDbBroker
{
    private readonly HttpClient httpClient;
    private readonly ILogger<PotterDbBroker> logger;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public PotterDbBroker(HttpClient httpClient, ILogger<PotterDbBroker> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async ValueTask<List<BookDto>> GetBooksAsync()
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.BooksEndpoint}";
            logger.LogInformation("Fetching books from {Url}", url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<BookResponse>>(url, JsonOptions);
            return response?.Data.Select(BookDto.FromResponse).ToList() ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching books from PotterDB");
            throw;
        }
    }

    public async ValueTask<BookDto> GetBookByIdAsync(string bookId)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.BooksEndpoint}/{bookId}";
            logger.LogInformation("Fetching book {BookId} from {Url}", bookId, url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<BookResponse>>(url, JsonOptions);
            var book = response?.Data.FirstOrDefault()
                ?? throw new InvalidOperationException($"Book {bookId} not found");

            return BookDto.FromResponse(book);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching book {BookId} from PotterDB", bookId);
            throw;
        }
    }

    public async ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.CharactersEndpoint}?page[number]={pageNumber}&page[size]={pageSize}";
            logger.LogInformation("Fetching characters from {Url} (page {PageNumber}, size {PageSize})", url, pageNumber, pageSize);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<CharacterResponse>>(url, JsonOptions);
            return response?.Data.Select(CharacterDto.FromResponse).ToList() ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching characters from PotterDB (page {PageNumber})", pageNumber);
            throw;
        }
    }

    public async ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.CharactersEndpoint}/{characterId}";
            logger.LogInformation("Fetching character {CharacterId} from {Url}", characterId, url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<CharacterResponse>>(url, JsonOptions);
            var character = response?.Data.FirstOrDefault()
                ?? throw new InvalidOperationException($"Character {characterId} not found");

            return CharacterDto.FromResponse(character);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching character {CharacterId} from PotterDB", characterId);
            throw;
        }
    }

    public async ValueTask<List<SpellDto>> GetSpellsAsync()
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.SpellsEndpoint}";
            logger.LogInformation("Fetching spells from {Url}", url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<SpellResponse>>(url, JsonOptions);
            return response?.Data.Select(SpellDto.FromResponse).ToList() ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching spells from PotterDB");
            throw;
        }
    }

    public async ValueTask<SpellDto> GetSpellByIdAsync(string spellId)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.SpellsEndpoint}/{spellId}";
            logger.LogInformation("Fetching spell {SpellId} from {Url}", spellId, url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<SpellResponse>>(url, JsonOptions);
            var spell = response?.Data.FirstOrDefault()
                ?? throw new InvalidOperationException($"Spell {spellId} not found");

            return SpellDto.FromResponse(spell);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching spell {SpellId} from PotterDB", spellId);
            throw;
        }
    }

    public async ValueTask<List<PotionDto>> GetPotionsAsync()
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.PotionsEndpoint}";
            logger.LogInformation("Fetching potions from {Url}", url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<PotionResponse>>(url, JsonOptions);
            return response?.Data.Select(PotionDto.FromResponse).ToList() ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching potions from PotterDB");
            throw;
        }
    }

    public async ValueTask<PotionDto> GetPotionByIdAsync(string potionId)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}{PotterDbConstants.PotionsEndpoint}/{potionId}";
            logger.LogInformation("Fetching potion {PotionId} from {Url}", potionId, url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<PotionResponse>>(url, JsonOptions);
            var potion = response?.Data.FirstOrDefault()
                ?? throw new InvalidOperationException($"Potion {potionId} not found");

            return PotionDto.FromResponse(potion);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching potion {PotionId} from PotterDB", potionId);
            throw;
        }
    }

    public async ValueTask<List<MovieDto>> GetMoviesAsync()
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}/movies";
            logger.LogInformation("Fetching movies from {Url}", url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<MovieResponse>>(url, JsonOptions);
            return response?.Data.Select(MovieDto.FromResponse).ToList() ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching movies from PotterDB");
            throw;
        }
    }

    public async ValueTask<MovieDto> GetMovieByIdAsync(string movieId)
    {
        try
        {
            string url = $"{PotterDbConstants.BaseUrl}/movies/{movieId}";
            logger.LogInformation("Fetching movie {MovieId} from {Url}", movieId, url);

            var response = await httpClient.GetFromJsonAsync<PotterDbResponse<MovieResponse>>(url, JsonOptions);
            var movie = response?.Data.FirstOrDefault()
                ?? throw new InvalidOperationException($"Movie {movieId} not found");

            return MovieDto.FromResponse(movie);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error fetching movie {MovieId} from PotterDB", movieId);
            throw;
        }
    }
}
