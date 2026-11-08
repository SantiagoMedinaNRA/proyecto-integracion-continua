using Api.Brokers;
using Api.Core.Services;
using Microsoft.Extensions.Logging;
using Shared.Constants;
using Shared.Models;

namespace Api.UnitTests.Core.Services;

public class PotionsServiceTests
{
    private readonly IPotterDbBroker potterDbBroker;
    private readonly ICacheBroker cacheBroker;
    private readonly ILogger<PotionsService> logger;
    private readonly PotionsService potionsService;

    public PotionsServiceTests()
    {
        potterDbBroker = Substitute.For<IPotterDbBroker>();
        cacheBroker = Substitute.For<ICacheBroker>();
        logger = Substitute.For<ILogger<PotionsService>>();
        potionsService = new PotionsService(potterDbBroker, cacheBroker, logger);
    }

    [Fact]
    public async Task GetPotionsAsync_WhenCacheMiss_FetchesFromBrokerAndCaches()
    {
        // Given
        var expectedPotions = new List<PotionDto>
        {
            new() { Id = "potion-1", Name = "Polyjuice Potion", Effect = "Transforms the drinker into another person" },
            new() { Id = "potion-2", Name = "Veritaserum", Effect = "Forces the drinker to tell the truth" }
        };

        cacheBroker.GetAsync<List<PotionDto>>(Arg.Any<string>()).Returns((List<PotionDto>?)null);
        potterDbBroker.GetPotionsAsync().Returns(expectedPotions);

        // When
        var result = await potionsService.GetPotionsAsync();

        // Then
        result.ShouldBe(expectedPotions);
        result.Count.ShouldBe(2);
        await potterDbBroker.Received(1).GetPotionsAsync();
        await cacheBroker.Received(1).SetAsync(CacheKeys.AllPotions, expectedPotions);
        logger.ReceivedWithAnyArgs().LogInformation("Fetching potions from PotterDB");
    }

    [Fact]
    public async Task GetPotionsAsync_WhenCacheHit_ReturnsCachedPotions()
    {
        // Given
        var cachedPotions = new List<PotionDto>
        {
            new() { Id = "potion-1", Name = "Polyjuice Potion" }
        };

        cacheBroker.GetAsync<List<PotionDto>>(CacheKeys.AllPotions).Returns(cachedPotions);

        // When
        var result = await potionsService.GetPotionsAsync();

        // Then
        result.ShouldBe(cachedPotions);
        result.Count.ShouldBe(1);
        await potterDbBroker.DidNotReceive().GetPotionsAsync();
        logger.ReceivedWithAnyArgs().LogInformation("Returning cached potions");
    }

    [Fact]
    public async Task GetPotionByIdAsync_WhenCacheMiss_FetchesFromBrokerAndCaches()
    {
        // Given
        var potionId = "potion-1";
        var expectedPotion = new PotionDto
        {
            Id = potionId,
            Name = "Polyjuice Potion",
            Effect = "Transforms the drinker into another person",
            Difficulty = "Advanced"
        };

        cacheBroker.GetAsync<PotionDto>(Arg.Any<string>()).Returns((PotionDto?)null);
        potterDbBroker.GetPotionByIdAsync(potionId).Returns(expectedPotion);

        // When
        var result = await potionsService.GetPotionByIdAsync(potionId);

        // Then
        result.ShouldBe(expectedPotion);
        result.Id.ShouldBe(potionId);
        result.Name.ShouldBe("Polyjuice Potion");
        await potterDbBroker.Received(1).GetPotionByIdAsync(potionId);
        logger.ReceivedWithAnyArgs().LogInformation("Fetching potion {PotionId} from PotterDB", potionId);
    }

    [Fact]
    public async Task GetPotionByIdAsync_WhenCacheHit_ReturnsCachedPotion()
    {
        // Given
        var potionId = "potion-1";
        var cachedPotion = new PotionDto { Id = potionId, Name = "Polyjuice Potion" };
        var cacheKey = string.Format(CacheKeys.PotionDetail, potionId);

        cacheBroker.GetAsync<PotionDto>(cacheKey).Returns(cachedPotion);

        // When
        var result = await potionsService.GetPotionByIdAsync(potionId);

        // Then
        result.ShouldBe(cachedPotion);
        await potterDbBroker.DidNotReceive().GetPotionByIdAsync(Arg.Any<string>());
        logger.ReceivedWithAnyArgs().LogInformation("Returning cached potion {PotionId}", potionId);
    }

    [Fact]
    public async Task GetPotionsAsync_WhenBrokerThrows_LogsErrorAndRethrows()
    {
        // Given
        cacheBroker.GetAsync<List<PotionDto>>(Arg.Any<string>()).Returns((List<PotionDto>?)null);
        potterDbBroker.GetPotionsAsync().Returns<ValueTask<List<PotionDto>>>(x => { throw new Exception("PotterDB unavailable"); });

        // When & Then
        var act = async () => await potionsService.GetPotionsAsync();
        await act.ShouldThrowAsync<Exception>();
        logger.ReceivedWithAnyArgs().LogError(Arg.Any<Exception>(), "Error retrieving potions");
    }
}