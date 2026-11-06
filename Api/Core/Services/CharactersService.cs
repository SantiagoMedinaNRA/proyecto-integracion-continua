namespace Api.Core.Services;

public class CharactersService : ICharactersService
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<CharactersService> logger;

    public CharactersService(IPotterDbBroker potterDbBroker, ICacheBroker cacheBroker, ILogger<CharactersService> logger)
    {
        this.potterDbBroker = potterDbBroker;
        this.cacheBroker = cacheBroker;
        this.logger = logger;
    }

    public async ValueTask<List<CharacterDto>> GetCharactersAsync(int pageNumber = 1, int pageSize = 20)
    {
        try
        {
            string cacheKey = string.Format(CacheKeys.CharacterPage, pageNumber, pageSize);
            var cachedCharacters = await cacheBroker.GetAsync<List<CharacterDto>>(cacheKey);

            if (cachedCharacters != null)
            {
                logger.LogInformation("Returning cached characters for page {PageNumber}", pageNumber);
                return cachedCharacters;
            }

            logger.LogInformation("Fetching characters from PotterDB (page {PageNumber}, size {PageSize})", pageNumber, pageSize);
            List<CharacterDto> characters = await potterDbBroker.GetCharactersAsync(pageNumber, pageSize);

            await cacheBroker.SetAsync(cacheKey, characters);

            return characters;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving characters for page {PageNumber}", pageNumber);
            throw;
        }
    }

    public async ValueTask<CharacterDto> GetCharacterByIdAsync(string characterId)
    {
        try
        {
            string cacheKey = string.Format(CacheKeys.CharacterDetail, characterId);
            var cachedCharacter = await cacheBroker.GetAsync<CharacterDto>(cacheKey);

            if (cachedCharacter != null)
            {
                logger.LogInformation("Returning cached character {CharacterId}", characterId);
                return cachedCharacter;
            }

            logger.LogInformation("Fetching character {CharacterId} from PotterDB", characterId);
            CharacterDto character = await potterDbBroker.GetCharacterByIdAsync(characterId);

            await cacheBroker.SetAsync(cacheKey, character);

            return character;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving character {CharacterId}", characterId);
            throw;
        }
    }
}
