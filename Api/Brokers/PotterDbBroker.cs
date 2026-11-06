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
}
