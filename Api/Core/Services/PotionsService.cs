namespace Api.Core.Services;

public class PotionsService : IPotionsService
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<PotionsService> logger;

    public PotionsService(IPotterDbBroker potterDbBroker, ICacheBroker cacheBroker, ILogger<PotionsService> logger)
    {
        this.potterDbBroker = potterDbBroker;
        this.cacheBroker = cacheBroker;
        this.logger = logger;
    }

    public async ValueTask<List<PotionDto>> GetPotionsAsync()
    {
        try
        {
            var cachedPotions = await cacheBroker.GetAsync<List<PotionDto>>(CacheKeys.AllPotions);

            if (cachedPotions != null)
            {
                logger.LogInformation("Returning cached potions");
                return cachedPotions;
            }

            logger.LogInformation("Fetching potions from PotterDB");
            List<PotionDto> potions = await potterDbBroker.GetPotionsAsync();

            await cacheBroker.SetAsync(CacheKeys.AllPotions, potions);

            return potions;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving potions");
            throw;
        }
    }

    public async ValueTask<PotionDto> GetPotionByIdAsync(string potionId)
    {
        try
        {
            string cacheKey = string.Format(CacheKeys.PotionDetail, potionId);
            var cachedPotion = await cacheBroker.GetAsync<PotionDto>(cacheKey);

            if (cachedPotion != null)
            {
                logger.LogInformation("Returning cached potion {PotionId}", potionId);
                return cachedPotion;
            }

            logger.LogInformation("Fetching potion {PotionId} from PotterDB", potionId);
            PotionDto potion = await potterDbBroker.GetPotionByIdAsync(potionId);

            await cacheBroker.SetAsync(cacheKey, potion);

            return potion;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error retrieving potion {PotionId}", potionId);
            throw;
        }
    }
}
