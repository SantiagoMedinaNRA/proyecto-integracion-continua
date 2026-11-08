namespace Api.Core.Services;

public class SpellsService : ISpellsService
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<SpellsService> logger;

    public SpellsService(IPotterDbBroker potterDbBroker, ICacheBroker cacheBroker, ILogger<SpellsService> logger)
    {
        this.potterDbBroker = potterDbBroker;
        this.cacheBroker = cacheBroker;
        this.logger = logger;
    }

    public async ValueTask<List<SpellDto>> GetSpellsAsync()
    {
        try
        {
            var cachedSpells = await cacheBroker.GetAsync<List<SpellDto>>(CacheKeys.AllSpells);

            if (cachedSpells != null)
            {
                logger.LogInformation("Returning cached spells");
                return cachedSpells;
            }

            logger.LogInformation("Fetching spells from PotterDB");
            List<SpellDto> spells = await potterDbBroker.GetSpellsAsync();

            await cacheBroker.SetAsync(CacheKeys.AllSpells, spells);

            return spells;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving spells");
            throw;
        }
    }

    public async ValueTask<SpellDto> GetSpellByIdAsync(string spellId)
    {
        try
        {
            string cacheKey = string.Format(CacheKeys.SpellDetail, spellId);
            var cachedSpell = await cacheBroker.GetAsync<SpellDto>(cacheKey);

            if (cachedSpell != null)
            {
                logger.LogInformation("Returning cached spell {SpellId}", spellId);
                return cachedSpell;
            }

            logger.LogInformation("Fetching spell {SpellId} from PotterDB", spellId);
            SpellDto spell = await potterDbBroker.GetSpellByIdAsync(spellId);

            await cacheBroker.SetAsync(cacheKey, spell);

            return spell;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving spell {SpellId}", spellId);
            throw;
        }
    }
}
